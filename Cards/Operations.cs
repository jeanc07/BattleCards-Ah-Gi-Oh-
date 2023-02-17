namespace Cards
{
    public class Operations
    {
        public static bool InvocarEspecial(Player player, int cardId,Card cardgen)
        {
            bool posible = false;

            for (int i = 0; i < player.Graveyard.Count; i++)
            {
                if(cardId == player.Graveyard.ElementAt(i).Id && player.Graveyard.ElementAt(i) is MonsterCard) 
                {
                    posible = true;
                    Card card = player.Graveyard.ElementAt(i);
                    player.Graveyard.RemoveAt(i);
                    player.MonsterField.Add(card);
                }                      
            }

            for (int i = 0; i < player.Deck.Count && posible == false; i++)
            {
                if(cardId == player.Deck.ElementAt(i).Id && player.Deck.ElementAt(i) is MonsterCard)   
                {
                    posible = true;
                    Card card = player.Deck.ElementAt(i);
                    player.Deck.RemoveAt(i);
                    player.MonsterField.Add(card);                    
                }
            }


            for (int i = 0; i < player.Hand.Count && posible == false; i++)
            {
                if(cardId == player.Hand.ElementAt(i).Id && player.Hand.ElementAt(i) is MonsterCard)   
                {
                    posible = true;
                    Card card = player.Hand.ElementAt(i);
                    player.Hand.RemoveAt(i);
                    player.MonsterField.Add(card);                        
                }
            }

            if (posible && cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }

            return posible;
        }        
    
        public static bool InvocarNormal(Player player, int n, List<int> sacrifice, bool position = false)   //false significa ataque
        {
            Card temp = new Card();
            bool posible = false;
            if((player.Hand[n] is TrapCard || player.Hand[n] is MagicCard) && player.MagicField.Count < 5)
            {
                    temp = player.Hand[n];
                    player.MagicField.Add(temp);
                    player.Hand.RemoveAt(n);
                    posible = true;
            }
            else if(player.Hand[n] is MonsterCard && player.CanInvoke == true && player.MonsterField.Count < 5)
            {
                posible = true;
                temp = player.Hand[n];
                ((MonsterCard)temp).Position = position;
                if(((MonsterCard)temp).Position == true)
                    ((MonsterCard)temp).CanATK = false;
                player.MonsterField.Add(temp);
                player.Hand.RemoveAt(n);       
                player.CanInvoke = false; 
            }

            return posible;
        }  

        public static bool Destruir(Player player, Player playerAdversary, int cardId,Card cardgen)
        {
            bool posible = false;

            for (int i = 0; i < playerAdversary.MonsterField.Count && posible == false; i++)
            {
                if(playerAdversary.MonsterField.ElementAt(i).Id == cardId)
                {
                    playerAdversary.Graveyard.Add(playerAdversary.MonsterField.ElementAt(i));
                    playerAdversary.MonsterField.RemoveAt(i);
                    posible = true;
                }
            }
            for (int i = 0; i < playerAdversary.MagicField.Count && posible == false; i++)
            {
                if (playerAdversary.MagicField.ElementAt(i).Id == cardId)
                {
                    playerAdversary.Graveyard.Add(playerAdversary.MagicField.ElementAt(i));
                    playerAdversary.MagicField.RemoveAt(i);
                    posible = true;
                }
            }

            if (posible && cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }

            return posible;
        }
    
        public static void ReducirVida(Player player, Player playerAdversary,Card cardgen, int reduccion = 500)
        {
            playerAdversary.Vida-=reduccion;

            if (cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }
        }
    
        public static bool ReducirAtaque(Player player,Card cardgen,Player playerAdversary, int cardId, int reduccion = 500)
        {
            bool posible = false;

            for (int i = 0; i < playerAdversary.MonsterField.Count && posible == false; i++)
            {
                if(playerAdversary.MonsterField.ElementAt(i).Id == cardId)
                {
                    ((MonsterCard)playerAdversary.MonsterField.ElementAt(i)).ATK-=reduccion;
                    posible = true;
                }
            }

            if (posible && cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }

            return posible;
        }
    
        public static void Atacar(Player p1, Player p2, int n, int k)
        {
            MonsterCard mcp1 = ((MonsterCard)p1.MonsterField[n]);
            MonsterCard mcp2 = new MonsterCard();

            if(k >= 0)
                mcp2 = ((MonsterCard)p2.MonsterField[k]);
            Card monstertemp = new Card();
            if (((MonsterCard)p1.MonsterField.ElementAt(n)).CanATK == true)
            {
                if(p2.MonsterField.Count > 0) 
                {

                    if(mcp1.ATK < mcp2.ATK && mcp2.Position == false)
                    {
                        if (p1.MonsterField.Count > 0)
                            ((MonsterCard)p1.MonsterField.ElementAt(n)).CanATK = false;
                        p1.Graveyard.Add(mcp1);
                        p1.MonsterField.RemoveAt(n);
                        p1.Vida-=(mcp2.ATK-mcp1.ATK);
                    }
                    else if(mcp1.ATK > mcp2.ATK && mcp2.Position == false)
                    {
                        if (p1.MonsterField.Count > 0)
                            ((MonsterCard)p1.MonsterField.ElementAt(n)).CanATK = false;
                        p2.Graveyard.Add(mcp2);
                        p2.MonsterField.RemoveAt(k);
                        p2.Vida-=(mcp1.ATK-mcp2.ATK);
                    }
                    else if(mcp1.ATK == mcp2.ATK && mcp2.Position == false) 
                    {
                        if (p1.MonsterField.Count > 0)
                            ((MonsterCard)p1.MonsterField.ElementAt(n)).CanATK = false;
                        p1.Graveyard.Add(mcp1);
                        p1.MonsterField.RemoveAt(n);

                        p2.Graveyard.Add(mcp2);
                        p2.MonsterField.RemoveAt(k);
                    }
                    if(mcp1.ATK > mcp2.DEF && mcp2.Position == true) 
                    {
                        if (p1.MonsterField.Count > 0)
                            ((MonsterCard)p1.MonsterField.ElementAt(n)).CanATK = false;
                        p2.Graveyard.Add(mcp2);
                        p2.MonsterField.RemoveAt(k);
                    }
                    if(mcp1.ATK <= mcp2.DEF && mcp2.Position == true) 
                    {
                        if (p1.MonsterField.Count > 0)
                            ((MonsterCard)p1.MonsterField.ElementAt(n)).CanATK = false;
                        p1.Graveyard.Add(mcp1);
                        p1.MonsterField.RemoveAt(n);
                    }
                }
                else 
                {
                    if (p1.MonsterField.Count > 0)
                        ((MonsterCard)p1.MonsterField.ElementAt(n)).CanATK = false;
                    p2.Vida-=mcp1.ATK;
                }             
            }
        }

        public static void AtacarEspecial(Player p1, Player p2, int n, int k)
        {
            MonsterCard mcp1 = ((MonsterCard)p1.MonsterField[n]);
            MonsterCard mcp2 = new MonsterCard();

            if (k >= 0)
                mcp2 = ((MonsterCard)p2.MonsterField[k]);
            Card monstertemp = new Card();
            if (p2.MonsterField.Count > 0)
            {

                if (mcp1.ATK < mcp2.ATK && mcp2.Position == false)
                {
                    p1.Graveyard.Add(mcp1);
                    p1.MonsterField.RemoveAt(n);
                    p1.Vida -= (mcp2.ATK - mcp1.ATK);
                }
                else if (mcp1.ATK > mcp2.ATK && mcp2.Position == false)
                {
                    p2.Graveyard.Add(mcp2);
                    p2.MonsterField.RemoveAt(k);
                    p2.Vida -= (mcp1.ATK - mcp2.ATK);
                }
                else if (mcp1.ATK == mcp2.ATK && mcp2.Position == false)
                {
                    p1.Graveyard.Add(mcp1);
                    p1.MonsterField.RemoveAt(n);

                    p2.Graveyard.Add(mcp2);
                    p2.MonsterField.RemoveAt(k);
                }
                if (mcp1.ATK > mcp2.DEF && mcp2.Position == true)
                {
                    p2.Graveyard.Add(mcp2);
                    p2.MonsterField.RemoveAt(k);
                }
                if (mcp1.ATK <= mcp2.DEF && mcp2.Position == true)
                {
                    p1.Graveyard.Add(mcp1);
                    p1.MonsterField.RemoveAt(n);
                }
            }
            else
            {
                p2.Vida -= mcp1.ATK;
            }
        }

        public static bool Cambiar(Player player,Card cardgen,Player playerAdversary, int cardId)
        {
            bool posible = false;

            for (int i = 0; i < playerAdversary.MonsterField.Count && posible == false; i++)
            {
                if(playerAdversary.MonsterField.ElementAt(i).Id == cardId)
                {
                    if(((MonsterCard)playerAdversary.MonsterField.ElementAt(i)).Position == false)
                        ((MonsterCard)playerAdversary.MonsterField.ElementAt(i)).Position = true;
                    else
                        ((MonsterCard)playerAdversary.MonsterField.ElementAt(i)).Position = false;

                    posible = true;
                }
            }

            if (posible && cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }

            return posible;
        }        
    
        public static bool Draw(Player player,Card cardgen,int n = 1)
        {
            bool posible = false;

            Card temp;
            for(int i = 0; i < n; i++)
            {
                temp = player.Deck[player.Deck.Count-1];
                player.Hand.Add(temp);
                player.Deck.RemoveAt(player.Deck.Count-1);
            }
            posible = true;

            if (posible && cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }

            return posible;
        }  
        public static void DrawNormal(Player p,int n = 1)
        {
            Card temp;
            for(int i = 0; i < n; i++)
            {
                temp = p.Deck[p.Deck.Count-1];
                p.Hand.Add(temp);
                p.Deck.RemoveAt(p.Deck.Count-1);
            }
        }      
    
        public static bool Revivir(Player player,Card cardgen, int cardId)
        {
            bool posible = false;

            for (int i = 0; i < player.Graveyard.Count; i++)
            {
                if(cardId == player.Graveyard.ElementAt(i).Id && player.Graveyard.ElementAt(i) is MonsterCard) 
                {
                    posible = true;
                    Card card = player.Graveyard.ElementAt(i);
                    player.Graveyard.RemoveAt(i);
                    player.MonsterField.Add(card);
                }                      
            }
            if (posible && cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }

            return posible;
        }    

        public static bool Controlar(Player player,Card cardgen, Player playerAdversary, int cardId)
        {
            bool posible = false;

            if(player.MonsterField.Count < 5)
            {
                for (int i = 0; i < playerAdversary.MonsterField.Count; i++)
                {
                    if(cardId == playerAdversary.MonsterField.ElementAt(i).Id) 
                    {
                        posible = true;
                        Card card = playerAdversary.MonsterField.ElementAt(i);
                        playerAdversary.MonsterField.RemoveAt(i);
                        player.MonsterField.Add(card);
                    }
                }
            }

            if (posible && cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }
            return posible;
        }         
    
        public static bool IncrementarAtaque(Player player,Card cardgen, int cardId, int aumento = 500)
        {
            bool posible = false;

            for (int i = 0; i < player.MonsterField.Count && posible == false; i++)
            {
                if(player.MonsterField.ElementAt(i).Id == cardId)
                {
                    ((MonsterCard)player.MonsterField.ElementAt(i)).ATK+=aumento;
                    posible = true;
                }
            }

            if (posible && cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }

            return posible;
        }
    
        public static bool Subir(Player player, Card cardgen,Player playerAdversary, int cardId)
        {
            bool posible = false;

            for (int i = 0; i < playerAdversary.MonsterField.Count && posible == false; i++)
            {
                if(playerAdversary.MonsterField.ElementAt(i).Id == cardId)
                {
                    playerAdversary.Hand.Add(playerAdversary.MonsterField.ElementAt(i));
                    playerAdversary.MonsterField.RemoveAt(i);
                    posible = true;
                }
            }

            if (posible && cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }

            return posible;
        }    
    
        public static bool DevolverAtaque(Player player,Card cardgen ,Player playerAdversary, int cardId)
        {
            bool posible = false;

            for (int i = 0; i < playerAdversary.MonsterField.Count && posible == false; i++)
            {
                if(playerAdversary.MonsterField.ElementAt(i).Id == cardId)
                {
                    ReducirVida(player,playerAdversary,cardgen, ((MonsterCard)playerAdversary.MonsterField.ElementAt(i)).ATK);
                    posible = true;
                }
            }

            if (posible && cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }

            return posible;
        }        
    
        public static void RobarVida(Player player,Card cardgen, Player playerAdversary, int vida = 500)
        {
            playerAdversary.Vida-=vida;
            player.Vida+=vida;

            if (cardgen is MagicCard || cardgen is TrapCard)
            {
                player.MagicField.Remove(cardgen);
                player.MagicField.Remove(cardgen);
            }
        }        

        public static void ChangeTurnToPlay(Player player)
        {
            for (int i = 0; i < player.MonsterField.Count; i++)
            {
                ((MonsterCard)player.MonsterField.ElementAt(i)).CanATK = true;
                ((MonsterCard)player.MonsterField.ElementAt(i)).CanEffect = true;
            }
            player.CanInvoke = true;
        }

        public static void ChangeTurn(Player player)
        {
            for (int i = 0; i < player.MonsterField.Count; i++)
            {
                ((MonsterCard)player.MonsterField.ElementAt(i)).CanATK = false;
                ((MonsterCard)player.MonsterField.ElementAt(i)).CanEffect = false;
            }
            player.CanInvoke = false;
        }        
    }
}