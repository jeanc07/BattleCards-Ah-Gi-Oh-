namespace Cards
{
    public class IABot
    {
        private int count;

        public IABot(int count)
        {
            this.count = count;
        }

        public  void IAStart(Player IA, Player adversary)
        {
            Operations.DrawNormal(IA);
            InvokePhase(IA);
            if(count != 0)
                BattlePhase(IA,adversary);
            EndPhase(IA);
        }

        public int SacrificeCount(Card card)
        {
            int result = 0;
            if (((MonsterCard)card).Nivel > 5)
                result = 1;
       
            return result;
        }

        /*public  bool SacrificePermission(List<int> sacrifice, Player player,int n)
        {
            if(sacrifice.Count > 1)
            {
                return checkMonsterLevel(player,n,sacrifice);               
            }
            return false;
        }*/

        public  Card  checkMonsterLevel(Player p)
        {
            Card res = new MonsterCard();
            for(int i = 0; i < p.Hand.Count; i++)
            {
                if (p.Hand[i] is MonsterCard && ((MonsterCard)p.Hand[i]).Nivel > 5)
                    res = p.Hand[i];
            }

            return res;
        }

        private Card CheckMonsterfield(Player p)
        {
            Card res = new MonsterCard();
            for(int i = 0; i< p.MonsterField.Count; i++)
            {
                if (((MonsterCard)p.MonsterField[i]).Nivel < 5)
                    res = p.MonsterField[i];
            }
            return res;
        }

        private  void InvokePhase(Player IA)
        {
            List<int> sacrificemonster = new List<int>();
            int index = 0;
            Dictionary<Card,int> monster = new Dictionary<Card, int>();  // Cartas mounstruos de la mano contra indice en mano
            Card monstertemp = new Card();
            int monsterindex = 0;
            for (int i = 0; i < IA.Hand.Count; i++)
            {
                if (IA.Hand[i] is TrapCard || IA.Hand[i] is MagicCard)
                {
                    if (IA.MagicField.Count < 5)
                    {
                        Operations.InvocarNormal(IA,i,sacrificemonster);
                    }
                }
                else if(IA.Hand.ElementAt(i) is MonsterCard && ((MonsterCard)IA.Hand.ElementAt(i)).Nivel < 5)
                {
                    index = i;
                    monster.Add(IA.Hand.ElementAt(i),index);
                    monsterindex = index;
                }
            }
            index = 0;
            Card monsterhigh = checkMonsterLevel(IA);
            int temp = SacrificeCount(monsterhigh);
            if (monster.Count > 0)
            {
                Card monsterlow = CheckMonsterfield(IA);
                monstertemp = monster.Keys.ElementAt(0);
                for (int i = 1; i < monster.Count; i++)
                {
                    if (IA.MonsterField.Count > 0 && monsterlow.Nombre != "" && monsterhigh.Nombre != "")
                    {
                        IA.MonsterField.Add(monsterhigh);
                        IA.Graveyard.Add(monsterlow);
                        IA.MonsterField.Remove(monsterlow);
                        IA.Hand.Remove(monsterhigh);
                        IA.CanInvoke = false;
                    }
                    else if (((MonsterCard)monster.Keys.ElementAt(i)).ATK > ((MonsterCard)monstertemp).ATK && ((MonsterCard)monster.Keys.ElementAt(i)).Nivel <= 5)
                    {
                        monstertemp = monster.Keys.ElementAt(i);
                        monsterindex = monster.Values.ElementAt(i);
                        index = i;
                    }
                }
                if (((MonsterCard)monster.Keys.ElementAt(index)).ATK <= ((MonsterCard)monster.Keys.ElementAt(index)).DEF  && ((MonsterCard)monster.Keys.ElementAt(index)).Nivel <= 5 && ((MonsterCard)monster.Keys.ElementAt(index)).ATK < 1500 && temp == 0)
                {
                    Operations.InvocarNormal(IA, monsterindex, sacrificemonster, true);
                }
                else if(IA.CanInvoke == true)
                {
                    Operations.InvocarNormal(IA, monsterindex, sacrificemonster, false);
                }

            }
        }

        private  void BattlePhase(Player IA, Player adversary)
        {
            if (IA.MonsterField.Count == 0)
            {

            }else if (adversary.MonsterField.Count != 0)
            {
                Card minatkmonsteradversary = new Card();
                Card maxatkmonsterIA = new Card();
                Card minatkmonsterIA = new Card();
                Card maxatkmonsteradversary = new Card();
                Card mindefmonsteradversary = new Card();
                Card maxdefmonsterIA = new Card();
                Card mindefmonsterIA = new Card();
                Card maxdefmonsteradversary = new Card();
                minatkmonsteradversary = adversary.MonsterField.ElementAt(0);
                maxatkmonsterIA = IA.MonsterField.ElementAt(0);
                minatkmonsterIA = IA.MonsterField.ElementAt(0);
                maxatkmonsteradversary = adversary.MonsterField.ElementAt(0);
                mindefmonsteradversary = adversary.MonsterField.ElementAt(0);
                maxdefmonsterIA = IA.MonsterField.ElementAt(0);
                mindefmonsterIA = IA.MonsterField.ElementAt(0);
                maxdefmonsteradversary = adversary.MonsterField.ElementAt(0);
                int index1 = 0, index2 = 0, index3 = 0, index4 = 0, index5 = 0, index6 = 0, index7 = 0, index8 = 0;
                for (int i = 1; i < IA.MonsterField.Count; i++)
                {
                    if (((MonsterCard)IA.MonsterField.ElementAt(i)).ATK > ((MonsterCard)maxatkmonsterIA).ATK)
                    {
                        maxatkmonsterIA = IA.MonsterField.ElementAt(i);
                        index2 = i;
                    }
                    if (((MonsterCard)IA.MonsterField.ElementAt(i)).ATK < ((MonsterCard)minatkmonsterIA).ATK)
                    {
                        minatkmonsterIA = IA.MonsterField.ElementAt(i);
                        index3 = i;
                    }
                    if (((MonsterCard)IA.MonsterField.ElementAt(i)).DEF > ((MonsterCard)maxdefmonsterIA).DEF)
                    {
                        maxdefmonsterIA = IA.MonsterField.ElementAt(i);
                        index6 = i;
                    }
                    if (((MonsterCard)IA.MonsterField.ElementAt(i)).DEF < ((MonsterCard)mindefmonsterIA).DEF)
                    {
                        mindefmonsterIA = IA.MonsterField.ElementAt(i);
                        index7 = i;
                    }
                }
                for (int i = 0; i < adversary.MonsterField.Count; i++)
                {
                    if (((MonsterCard)adversary.MonsterField.ElementAt(i)).ATK < ((MonsterCard)minatkmonsteradversary).ATK)
                    {
                        minatkmonsteradversary = adversary.MonsterField.ElementAt(i);
                        index1 = i;
                    }
                    if (((MonsterCard)adversary.MonsterField.ElementAt(i)).ATK > ((MonsterCard)maxatkmonsteradversary).ATK)
                    {
                        maxatkmonsteradversary = adversary.MonsterField.ElementAt(i);
                        index4 = i;
                    }
                    if (((MonsterCard)adversary.MonsterField.ElementAt(i)).DEF > ((MonsterCard)maxdefmonsteradversary).DEF)
                    {
                        maxdefmonsteradversary = adversary.MonsterField.ElementAt(i);
                        index8 = i;
                    }
                    if (((MonsterCard)adversary.MonsterField.ElementAt(i)).DEF < ((MonsterCard)mindefmonsteradversary).DEF)
                    {
                        mindefmonsteradversary = adversary.MonsterField.ElementAt(i);
                        index5 = i;
                    }
                }
                if (((MonsterCard)minatkmonsteradversary).ATK < ((MonsterCard)maxatkmonsterIA).ATK)
                {
                    Operations.Atacar(IA, adversary, index2, index1);
                }
                else if (((MonsterCard)minatkmonsteradversary).ATK >= ((MonsterCard)maxatkmonsterIA).ATK)
                {
                    for (int i = 0; i < IA.MonsterField.Count; i++)
                    {
                        ((MonsterCard)IA.MonsterField.ElementAt(i)).Position = true;
                    }
                }
                else if ((((MonsterCard)maxdefmonsteradversary).DEF < ((MonsterCard)maxatkmonsterIA).ATK) && FindATKmonsters(adversary))
                {
                    Operations.Atacar(IA, adversary, index2, index8);
                }
            }else
            {
                for (int i = 0; i < IA.MonsterField.Count; i++)
                {
                    if (IA.MonsterField[i].Position == false && ((MonsterCard)IA.MonsterField[i]).CanATK == true)
                        Operations.Atacar(IA, adversary, i, -1);
                }
            }


        }
        public  void EndPhase(Player IA)
        {
            Operations.ChangeTurn(IA);
        }

        public  bool FindATKmonsters(Player pj)   // False si encuentra un mounstruo en posicion de ATK , True si todos los monstruos estan en DEF
        {
            bool result = true;
            for (int i = 0; i < pj.MonsterField.Count && result == true; i++)
            {
                if (((MonsterCard)pj.MonsterField.ElementAt(i)).Position == false)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}