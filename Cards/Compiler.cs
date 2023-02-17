namespace Cards
{
    public class Compiler
    {
        private List<string> symbols;

        private List<string> tokens;

        private List<string> operators;

        private List<string> conditionOperators;

        private List<string> condicionToken;        

        private List<string> specialTokens; 

        private List<Card> cards;

        private Player p1; 
        
        private Player p2;

        public Compiler(Player p1, Player p2)
        {
            this.p1 = p1;
            this.p2 = p2;

            symbols = new List<string>();
            symbols.Add("{");symbols.Add("}");symbols.Add(";");

            operators = new List<string>();
            operators.Add("*");
            operators.Add("/"); 
            operators.Add("-"); 
            operators.Add("+");
            

            conditionOperators = new List<string>();
            conditionOperators.Add("<=");
            conditionOperators.Add(">=");
            conditionOperators.Add("==");
            conditionOperators.Add("!=");
            conditionOperators.Add("<"); conditionOperators.Add(">");            
            

            condicionToken = new List<string>();
            condicionToken.Add("if"); condicionToken.Add("else");

            tokens = new List<string>();
            tokens.Add("inv"); tokens.Add("sub");
            tokens.Add("red"); tokens.Add("eva");
            tokens.Add("rea"); tokens.Add("evp");
            tokens.Add("atq"); tokens.Add("dev");
            tokens.Add("cam"); tokens.Add("rov");
            tokens.Add("roc"); tokens.Add("des");
            tokens.Add("rev");
            tokens.Add("con");
            tokens.Add("neg");
            tokens.Add("inc");   

            specialTokens = new List<string>();
            specialTokens.Add("via"); specialTokens.Add("vim");    

            cards = new List<Card>();
            string sourcepath = @"Content";
            List<string> temp = new List<string>(); 
            Card card = new Card(); 
            var arrayDirectory = Directory.EnumerateFiles(sourcepath);
            for (int j = 0; j < arrayDirectory.Count(); j++)
            {
                
                temp = File.ReadAllLines(arrayDirectory.ElementAt(j)).ToList();
                
                if(temp.ElementAt(1) == "Monster")
                {
                    card = new MonsterCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(5), temp.ElementAt(4), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(9))),
                                        temp.ElementAt(10),temp.ElementAt(11),true,Convert.ToInt32(temp.ElementAt(6)), Convert.ToInt32(temp.ElementAt(7)), Convert.ToInt32(temp.ElementAt(8)), true);
                }
                else if (temp.ElementAt(1) == "Magic")
                {
                    card = new MagicCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(4), temp.ElementAt(5), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(6))),temp.ElementAt(7),temp.ElementAt(8),true);
                }
                else if (temp.ElementAt(1) == "Trap")
                {
                    card = new TrapCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(4), temp.ElementAt(5), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(6))),temp.ElementAt(7),temp.ElementAt(8),true);
                }

                cards.Add(card);
            }

        }
        public Compiler()
        {
            this.p1 = new Player();
            this.p2 = new Player();

            symbols = new List<string>();
            symbols.Add("{"); symbols.Add("}"); symbols.Add(";");

            operators = new List<string>();
            operators.Add("*");
            operators.Add("/");
            operators.Add("-");
            operators.Add("+");


            conditionOperators = new List<string>();
            conditionOperators.Add("<=");
            conditionOperators.Add(">=");
            conditionOperators.Add("==");
            conditionOperators.Add("!=");
            conditionOperators.Add("<"); conditionOperators.Add(">");
         

            condicionToken = new List<string>();
            condicionToken.Add("if"); condicionToken.Add("else");

            tokens = new List<string>();
            tokens.Add("inv"); tokens.Add("sub");
            tokens.Add("red"); tokens.Add("eva");
            tokens.Add("rea"); tokens.Add("evp");
            tokens.Add("atq"); tokens.Add("dev");
            tokens.Add("cam"); tokens.Add("rov");
            tokens.Add("roc"); tokens.Add("des");
            tokens.Add("rev");
            tokens.Add("con");
            tokens.Add("neg");
            tokens.Add("inc");

            specialTokens = new List<string>();
            specialTokens.Add("via"); specialTokens.Add("vim");

            cards = new List<Card>();
            string sourcepath = @"Content";
            List<string> temp = new List<string>();
            Card card = new Card();
            var arrayDirectory = Directory.EnumerateFiles(sourcepath);
            for (int j = 0; j < arrayDirectory.Count(); j++)
            {

                temp = File.ReadAllLines(arrayDirectory.ElementAt(j)).ToList();

                if (temp.ElementAt(1) == "Monster")
                {
                    card = new MonsterCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(5), temp.ElementAt(4), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(9))),
                                        temp.ElementAt(10),temp.ElementAt(11),true,Convert.ToInt32(temp.ElementAt(6)), Convert.ToInt32(temp.ElementAt(7)), Convert.ToInt32(temp.ElementAt(8)), true);
                }
                else if (temp.ElementAt(1) == "Magic")
                {
                    card = new MagicCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(4), temp.ElementAt(5), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(6))),temp.ElementAt(7),temp.ElementAt(8),true);
                }
                else if (temp.ElementAt(1) == "Trap")
                {
                    card = new TrapCard(Convert.ToInt32(temp.ElementAt(0)), temp.ElementAt(2), temp.ElementAt(3), temp.ElementAt(4), temp.ElementAt(5), Convert.ToBoolean(Convert.ToInt32(temp.ElementAt(6))),temp.ElementAt(7),temp.ElementAt(8),true);
                }

                cards.Add(card);
            }
        }
            public List<string> divideElements(string line)
        {
            List<string> elements = new List<string>();
            string wordToken = "";
            string wordNumber = "";
            string wordConditionOperator = "";
            for (int i = 0; i < line.Length; i++)
            {
                    if(line.ElementAt(i) >= 97 && line.ElementAt(i) <= 122)
                    {
                        if(wordNumber != ""){
                            elements.Add(wordNumber);
                            wordNumber = "";
                        }

                        if(wordConditionOperator != ""){
                            elements.Add(wordConditionOperator);
                            wordConditionOperator = "";
                        }

                        wordToken += line.ElementAt(i);             
                    }
                    else if(line.ElementAt(i) >= 48 && line.ElementAt(i) <= 57)
                    {
                        if(wordToken != ""){
                            elements.Add(wordToken);
                            wordToken = "";
                        }

                        if(wordConditionOperator != ""){
                            elements.Add(wordConditionOperator);
                            wordConditionOperator = "";
                        }

                        wordNumber += line.ElementAt(i);   
                    }
                    else if (operators.Contains(line.ElementAt(i).ToString()) || symbols.Contains(line.ElementAt(i).ToString()))
                    {
                        if(wordNumber != ""){
                            elements.Add(wordNumber);
                            wordNumber = "";
                        }

                        if(wordConditionOperator != ""){
                            elements.Add(wordConditionOperator);
                            wordConditionOperator = "";
                        }

                        if(wordToken != ""){
                            elements.Add(wordToken);
                            wordToken = "";                                                          
                        }

                        elements.Add(line.ElementAt(i).ToString());
                    }   
                    else if (conditionOperators.Contains(line.ElementAt(i).ToString()))
                    {
                        if(wordNumber != ""){
                            elements.Add(wordNumber);
                            wordNumber = "";
                        }

                        if(wordToken != ""){
                            elements.Add(wordToken);
                            wordToken = "";
                        }

                        if (line.ElementAt(i+1).ToString() == "=")
                        {
                            elements.Add(line.ElementAt(i).ToString()+line.ElementAt(i+1).ToString());
                            i++;
                        }
                        else
                        {
                            elements.Add(line.ElementAt(i).ToString());                            
                        }
                    }      
                    else
                    {
                        if(wordNumber != ""){
                            elements.Add(wordNumber);
                            wordNumber = "";
                        }

                        if(wordConditionOperator != ""){
                            elements.Add(wordConditionOperator);
                            wordConditionOperator = "";
                        }

                        if(wordToken != ""){
                            elements.Add(wordToken);
                            wordToken = "";
                        }

                        if (line.ElementAt(i).ToString() == "=" && line.ElementAt(i+1).ToString() == "=")
                        {
                            elements.Add(line.ElementAt(i).ToString()+line.ElementAt(i+1).ToString());
                            i++;
                            continue;
                        }         

                        if (line.ElementAt(i).ToString() == "!" && line.ElementAt(i+1).ToString() == "=")
                        {
                            elements.Add(line.ElementAt(i).ToString()+line.ElementAt(i+1).ToString());
                            i++;
                            continue;
                        }                                         

                        if(line.ElementAt(i).ToString() != " ")
                            elements.Add(line.ElementAt(i).ToString());                        
                    }                               
            }

            if(wordToken != ""){
                elements.Add(wordToken);
                wordToken = "";
            }            

            return elements;
        }        

        public bool syntaxisAnalysis (List<string> lines)
        {
            bool bad = false;
            string line;
            List<string> currentTokens;
            List<string> temp;

            for (int i = 0; i < lines.Count && bad == false; i++)
            {
                line = lines[i].Trim();
                currentTokens = divideElements(line);
  

                if(currentTokens.ElementAt(0) != "if" && currentTokens.ElementAt(0) != "else")  //Chequeo de ;
                {
                    if(currentTokens.ElementAt(currentTokens.Count-1) != ";"){
                        bad = true; Console.WriteLine("Mal ;");}

                    int contar1 = 0;
                    for (int j = 0; j < currentTokens.Count && bad == false; j++)
                    {
                        if(currentTokens.ElementAt(j) == ";")
                            contar1++;                       
                    }   

                    if(contar1 > 1)
                    {
                        bad = true; Console.WriteLine("Mal ;");
                    }                        
                }

                int contar2 = 0, contar3 = 0;
                int pos2 = 0, pos3 = 0;
                for (int j = 0; j < currentTokens.Count && bad == false; j++)                   //Chequeo de ( )
                {
                    if(currentTokens.ElementAt(j) == "{")
                    {
                        contar2++;    
                        pos2 = j;
                    }
                        
                    if(currentTokens.ElementAt(j) == "}")
                    {
                        contar3++;
                        pos3 = j;
                    }                                                  
                }    

                if(contar2 != contar3 && contar2 > 1 ){
                    bad = true; Console.WriteLine("Mal (1)");}
                else if(pos2 > pos3 || pos2 == pos3-1) {
                    bad = true; Console.WriteLine("Mal (2)"); }

                for (int j = 0; j < currentTokens.Count && bad == false; j++)
                {
                    if(!symbols.Contains(currentTokens.ElementAt(j)) && !tokens.Contains(currentTokens.ElementAt(j)) && 
                        !operators.Contains(currentTokens.ElementAt(j)) && !condicionToken.Contains(currentTokens.ElementAt(j)) &&
                        !conditionOperators.Contains(currentTokens.ElementAt(j)) && !specialTokens.Contains(currentTokens.ElementAt(j)) &&
                        !int.TryParse(currentTokens.ElementAt(j), out _)){
                        bad = true; Console.WriteLine("Mal element (" + currentTokens.ElementAt(j) + ")"); }
                }

                if(bad == false)
                {
                    if(int.TryParse(currentTokens.ElementAt(0), out _) || symbols.Contains(currentTokens.ElementAt(0)) || 
                        operators.Contains(currentTokens.ElementAt(0)) ||conditionOperators.Contains(currentTokens.ElementAt(0)) ||
                        specialTokens.Contains(currentTokens.ElementAt(0))){   //Chequeo comienzo con operadores
                        bad = true;  Console.WriteLine("Mal comienzo"); }
                    else
                    {
                        if(currentTokens.ElementAt(0) == "else")                                            //Chequeo de instruccion else
                        {
                            if(i < 2){
                                bad = true;   Console.WriteLine("Mal else 1"); }
                            else
                            {
                                if(divideElements(lines.ElementAt(i-2)).ElementAt(0) != "if") {
                                    bad = true; Console.WriteLine("Mal else 2"); }
                            }

                            if(currentTokens.Count > 1) {
                                bad = true; Console.WriteLine("Mal else 3"); }
                        }
                        else if(currentTokens.ElementAt(0) == "if")                                                         //Chequeo de instruccion if
                        {
                            if(currentTokens.ElementAt(1) != "{" || currentTokens.ElementAt(currentTokens.Count - 1) != "}") {
                                bad = true; Console.WriteLine("Mal if 1"); }
                            else
                            {
                                temp = currentTokens.GetRange(2, currentTokens.Count-3);
                                if(temp.Count%2==0) {
                                    bad = true; Console.WriteLine("Mal if 2"); }
                                else
                                {
                                    int cantidadConditionOperator = 0;
                                    for (int k = 0; k < temp.Count; k++)
                                    {
                                        if(k%2 == 0)
                                        {
                                            if(!specialTokens.Contains(temp.ElementAt(k)) && !int.TryParse(temp.ElementAt(k), out _)) {
                                                bad = true; Console.WriteLine("Mal if 3 " + temp.ElementAt(k)); }
                                        }
                                        else
                                        {
                                            if(!operators.Contains(temp.ElementAt(k)) && !conditionOperators.Contains(temp.ElementAt(k))) {
                                                bad = true;      Console.WriteLine("Mal if 4"); }                                       
                                        }                                        

                                        if(conditionOperators.Contains(temp.ElementAt(k)))
                                        {
                                            cantidadConditionOperator++;
                                        }
                                    }

                                    if(cantidadConditionOperator != 1)
                                    {
                                        bad = true; Console.WriteLine("Mal if 5"); 
                                    }
                                }
                            }
                        }
                        else
                        {
                            List<string> afterSpecialToken = new List<string>(); afterSpecialToken.AddRange(operators); afterSpecialToken.Add("}");  afterSpecialToken.Add(";");
                            List<string> afterToken = new List<string>(); afterToken.Add(";"); afterToken.Add("{");
                            List<string> afterOperator = new List<string>(); afterOperator.AddRange(specialTokens);afterOperator.Add("{"); 
                            List<string> afterOpenParenthesis = new List<string>(); afterOpenParenthesis.AddRange(specialTokens); 
                            List<string> afterCloseParenthesis = new List<string>(); afterCloseParenthesis.AddRange(operators); afterCloseParenthesis.Add(";");
                            List<string> afterNumber = new List<string>(); afterNumber.AddRange(operators); afterNumber.Add("}"); afterNumber.Add(";");

                            for (int j = 0; j < currentTokens.Count - 1 && bad == false; j++)
                            {
                                if(tokens.Contains(currentTokens.ElementAt(j)))
                                {
                                    if(!afterToken.Contains(currentTokens.ElementAt(j+1)) && !int.TryParse(currentTokens.ElementAt(j+1), out _))
                                    {
                                        bad = true; Console.WriteLine("Mal line token ("+currentTokens.ElementAt(j+1)+")"); 
                                    }
                                }                                
                                if(specialTokens.Contains(currentTokens.ElementAt(j)))
                                {
                                    if(!afterSpecialToken.Contains(currentTokens.ElementAt(j+1)))
                                    {
                                        bad = true; Console.WriteLine("Mal line special token"); 
                                    }
                                }
                                else if(int.TryParse(currentTokens.ElementAt(j), out _))
                                {
                                    if(!afterNumber.Contains(currentTokens.ElementAt(j+1)))
                                    {
                                        bad = true; Console.WriteLine("Mal line number"); 
                                    }
                                }     
                                else if(operators.Contains(currentTokens.ElementAt(j)))
                                {
                                    if(!afterOperator.Contains(currentTokens.ElementAt(j+1)) && !int.TryParse(currentTokens.ElementAt(j+1), out _))
                                    {
                                        bad = true; Console.WriteLine("Mal line operator (" + currentTokens.ElementAt(j+1) + " " + i + ")"); 
                                    }
                                }   
                                else if(currentTokens.ElementAt(j) == "{")
                                {
                                    if(!afterOpenParenthesis.Contains(currentTokens.ElementAt(j+1)) && !int.TryParse(currentTokens.ElementAt(j+1), out _))
                                    {
                                        bad = true; Console.WriteLine("Mal line ("); 
                                    }
                                }       
                                else if(currentTokens.ElementAt(j) == "}")
                                {
                                    if(!afterCloseParenthesis.Contains(currentTokens.ElementAt(j+1)))
                                    {
                                        bad = true; Console.WriteLine("Mal line )"); 
                                    }
                                }                                                                                                                     
                            }
                        }                        
                    }
                }
            }

            return bad;
        } 

        public int doArithmetic(List<string> tokens)
        {
            int result = 0;
            int via1 = -1, via2 = -1;
            int vim1 = -1, vim2 = -1;
            int number1 = 0;
            int number2 = 0;
            bool exist = false;
            int pos = 0;

            if(tokens.ElementAt(0) == "{" && tokens.ElementAt(tokens.Count-1) == "}")
            {
                tokens.RemoveAt(0);
                tokens.RemoveAt(tokens.Count-1);
            }

            List<string> tempTokens = new List<string>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if(tokens.ElementAt(i) == "}")
                {
                    exist = false;
                    tokens.RemoveAt(i);
                    break;
                } 

                if(exist == true)
                {
                    tempTokens.Add(tokens.ElementAt(i));
                    tokens.RemoveAt(i);              
                }

                if(tokens.ElementAt(i) == "{")
                {
                    exist = true;
                    tokens.RemoveAt(i);
                    pos = i;
                } 

                if(exist == true)
                {
                    i--;                    
                }                             
            }    

            if(tempTokens.Count > 0)
            {
                result += doArithmetic(tempTokens);
                tokens.Insert(pos, result.ToString());
                tempTokens.Clear();
            }        

            for (int i = 0; i < tokens.Count-1; i++)
            {
                if(i%2!=0)
                {
                    if(tokens.ElementAt(i-1) == "via")
                        via1 = p2.Vida;
                    else if(tokens.ElementAt(i-1) == "vim")
                        vim1 = p1.Vida;    
                    else
                        number1 = int.Parse(tokens.ElementAt(i-1));                            

                    if(tokens.ElementAt(i+1) == "via")
                        via2 = p2.Vida;                          
                    else if(tokens.ElementAt(i+1) == "vim")
                        vim2 = p1.Vida; 
                    else
                        number2 = int.Parse(tokens.ElementAt(i+1)); 

                    
                    if(via1 != -1)  
                        result = via1;
                    else if(vim1 != -1)
                        result = vim1;
                    else
                        result = number1;

                    if(tokens.ElementAt(i) == "*")
                    { 
                        if(via2 != -1)  
                            result*= via2;
                        else if(vim2 != -1)
                            result*= vim2;
                        else
                            result*= number2;

                        tokens.RemoveAt(i-1);
                        tokens.RemoveAt(i-1);
                        tokens.RemoveAt(i-1);
                        if(i < tokens.Count)
                            tokens.Insert(i-1, result.ToString());
                        else
                            tokens.Add(result.ToString());
                        exist = true;
                        break;
                    }     

                    if(tokens.ElementAt(i) == "/")
                    { 
                        if(via2 != -1)  
                            result/= via2;
                        else if(vim2 != -1)
                            result/= vim2;
                        else
                            result/= number2;

                        tokens.RemoveAt(i-1);
                        tokens.RemoveAt(i-1);
                        tokens.RemoveAt(i-1);
                        if(i < tokens.Count)
                            tokens.Insert(i-1, result.ToString());
                        else
                            tokens.Add(result.ToString());
                        exist = true;
                        break;
                    }                                                                  
                }
            }

            if(tokens.Count > 1 && exist == false){
                for (int i = 0; i < tokens.Count-1; i++)
                {
                    if(i%2!=0)
                    {
                        if(tokens.ElementAt(i-1) == "via")
                            via1 = p2.Vida;
                        else if(tokens.ElementAt(i-1) == "vim")
                            vim1 = p1.Vida;    
                        else
                            number1 = int.Parse(tokens.ElementAt(i-1));                            

                        if(tokens.ElementAt(i+1) == "via")
                            via2 = p2.Vida;                          
                        else if(tokens.ElementAt(i+1) == "vim")
                            vim2 = p1.Vida; 
                        else
                            number2 = int.Parse(tokens.ElementAt(i+1)); 
                        
                        if(via1 != -1)  
                            result = via1;
                        else if(vim1 != -1)
                            result = vim1;
                        else
                            result = number1;    

                        if(tokens.ElementAt(i) == "+")
                        {
                            if(via2 != -1)  
                                result+= via2;
                            else if(vim2 != -1)
                                result+= vim2;
                            else
                                result+= number2;

                            tokens.RemoveAt(i-1);
                            tokens.RemoveAt(i-1);
                            tokens.RemoveAt(i-1);
                            if(i < tokens.Count)
                                tokens.Insert(i-1, result.ToString());
                            else
                                tokens.Add(result.ToString());
                            break;
                        }   

                        if(tokens.ElementAt(i) == "-")
                        {
                            if(via2 != -1)  
                                result-= via2;
                            else if(vim2 != -1)
                                result-= vim2;
                            else
                                result-= number2;

                            tokens.RemoveAt(i-1);
                            tokens.RemoveAt(i-1);
                            tokens.RemoveAt(i-1);
                            if(i < tokens.Count)
                                tokens.Insert(i-1, result.ToString());
                            else
                                tokens.Add(result.ToString());
                            break;
                        }                                                                   
                    }
                }      
            }      

            if(tokens.Count > 1){
                result = doArithmetic(tokens);
            }                

            return result;
        }

        public bool SemanticAnalysis(List<string> lines)
        {
            bool bad = false;
            string line;
            List<string> currentTokens;
            int result = 0;
            int ifResult = 0;    
            string token;       

            for (int i = 0; i < lines.Count; i++)
            {
                line = lines[i].Trim();
                currentTokens = divideElements(line);  
                token = currentTokens.ElementAt(0);

                if(token == "else")
                {
                    if(i+1 >= lines.Count)
                        bad = true;
                }
                if(token == "if")
                {
                    currentTokens.RemoveAt(0);
                    currentTokens.RemoveAt(0);
                    currentTokens.RemoveAt(currentTokens.Count - 1);

                    result = doArithmetic(currentTokens.TakeWhile(x => conditionOperators.IndexOf(x) == -1).ToList<string>());

                    int cantidad = currentTokens.TakeWhile(x => conditionOperators.IndexOf(x) == -1).ToList<string>().Count;
                    ifResult = doArithmetic(currentTokens.TakeLast(currentTokens.Count-(cantidad+1)).ToList<string>());

                    string comparator = currentTokens.ElementAt(cantidad);

                    currentTokens.Clear();
                    currentTokens.Add("if");
                    currentTokens.Add("{");
                    currentTokens.Add(result.ToString());
                    currentTokens.Add(comparator);
                    currentTokens.Add(ifResult.ToString());
                    currentTokens.Add("}");
                }
                else
                {
                    if(currentTokens.Count() > 1)
                    {
                        currentTokens.RemoveAt(0);
                        currentTokens.RemoveAt(currentTokens.Count - 1);
                    }
                    else
                    {
                        currentTokens.RemoveAt(0);
                    }


                    switch(token)
                    {
                        case "inv":
                        {
                            if(currentTokens.Count > 1)
                                bad = true;
                            break;
                        }
                        case "red":
                        {
                            if(currentTokens.Count > 1)
                            {
                                result = doArithmetic(currentTokens);

                                currentTokens.Clear();
                                currentTokens.Add(token);
                                currentTokens.Add(result.ToString());
                                currentTokens.Add(";");
                            }
                            else if(currentTokens.Count == 1)
                            {
                                currentTokens.Insert(0, token);
                                currentTokens.Add(";");                            
                            }
                            else
                            {
                                currentTokens.Add(token);
                                currentTokens.Add(";");                            
                            }   

                            if(currentTokens.Count != 2 && currentTokens.Count != 3)
                                bad = true;                
                            break;
                        }
                        case "rea":
                        {
                            if(currentTokens.Count > 1)
                            {
                                result = doArithmetic(currentTokens);

                                currentTokens.Clear();
                                currentTokens.Add(token);
                                currentTokens.Add(result.ToString());
                                currentTokens.Add(";");
                            }
                            else if(currentTokens.Count == 1)
                            {
                                currentTokens.Insert(0, token);
                                currentTokens.Add(";");                            
                            }
                            else
                            {
                                currentTokens.Add(token);
                                currentTokens.Add(";");                            
                            }   

                            if(currentTokens.Count != 2 && currentTokens.Count != 3)
                                bad = true;                             
                            break;
                        }
                        case "atq":
                        {
                            if(currentTokens.Count > 1)
                                bad = true;                       
                            break;
                        }
                        case "cam":
                        {
                            if(currentTokens.Count > 1)
                                bad = true;                             
                            break;
                        }   
                        case "roc":
                        {
                            if(currentTokens.Count > 1)
                                bad = true;                         
                            break;
                        }           
                        case "rev":
                        {
                            if(currentTokens.Count > 1)
                                bad = true;                         
                            break;
                        }        
                        case "con":
                        {
                            if(currentTokens.Count > 1)
                                bad = true;                        
                            break;
                        }       
                        case "inc":
                        {
                            if(currentTokens.Count > 1)
                            {
                                result = doArithmetic(currentTokens);

                                currentTokens.Clear();
                                currentTokens.Add(token);
                                currentTokens.Add(result.ToString());
                                currentTokens.Add(";");
                            }
                            else if(currentTokens.Count == 1)
                            {
                                currentTokens.Insert(0, token);
                                currentTokens.Add(";");                            
                            }
                            else
                            {
                                currentTokens.Add(token);
                                currentTokens.Add(";");                            
                            }   

                            if(currentTokens.Count != 2 && currentTokens.Count != 3)
                                bad = true;                                 
                            break;
                        }                                           
                        case "sub":
                        {
                            if(currentTokens.Count > 1)
                                bad = true;                          
                            break;
                        }            
                        case "dev":
                        {
                            if(currentTokens.Count > 1)
                                bad = true;  
                            break;
                        }   
                        case "rov":
                        {
                            if(currentTokens.Count > 1)
                            {
                                result = doArithmetic(currentTokens);

                                currentTokens.Clear();
                                currentTokens.Add(token);
                                currentTokens.Add(result.ToString());
                                currentTokens.Add(";");
                            }
                            else if(currentTokens.Count == 1)
                            {
                                currentTokens.Insert(0, token);
                                currentTokens.Add(";");                            
                            }
                            else
                            {
                                currentTokens.Add(token);
                                currentTokens.Add(";");                            
                            }                        

                            if(currentTokens.Count != 2 && currentTokens.Count != 3)
                                bad = true;                          
                            break;
                        } 
                        case "des":
                        {
                            if(currentTokens.Count > 1)
                                bad = true;                        
                            break;
                        }                                                                                                                                                                         
                    }      
                }
            }
            return bad;
        }


        public void UpdateEffects(CustomAttribute attribute)
        {
            string line;
            List<string> currentTokens;
            int result = 0;
            int ifResult = 0;    
            string token;       

            for (int i = 0; i < attribute.Lines.Count; i++)
            {
                line = attribute.Lines[i].Trim();
                currentTokens = divideElements(line);  
                token = currentTokens.ElementAt(0);

                if(token == "if")
                {
                    currentTokens.RemoveAt(0);
                    currentTokens.RemoveAt(0);
                    currentTokens.RemoveAt(currentTokens.Count - 1);

                    result = doArithmetic(currentTokens.TakeWhile(x => conditionOperators.IndexOf(x) == -1).ToList<string>());

                    int cantidad = currentTokens.TakeWhile(x => conditionOperators.IndexOf(x) == -1).ToList<string>().Count;
                    ifResult = doArithmetic(currentTokens.TakeLast(currentTokens.Count-(cantidad+1)).ToList<string>());

                    string comparator = currentTokens.ElementAt(cantidad);

                    currentTokens.Clear();
                    currentTokens.Add("if");
                    currentTokens.Add("{");
                    currentTokens.Add(result.ToString());
                    currentTokens.Add(comparator);
                    currentTokens.Add(ifResult.ToString());
                    currentTokens.Add("}");
                }
                else
                {
                    currentTokens.RemoveAt(0);
                    currentTokens.RemoveAt(currentTokens.Count - 1);

                    switch(currentTokens.ElementAt(0))
                    {
                        case "red":
                        {
                            if(currentTokens.Count > 1)
                            {
                                result = doArithmetic(currentTokens);

                                currentTokens.Clear();
                                currentTokens.Add(token);
                                currentTokens.Add(result.ToString());
                                currentTokens.Add(";");
                            }
                            else if(currentTokens.Count == 1)
                            {
                                currentTokens.Insert(0, token);
                                currentTokens.Add(";");                            
                            }
                            else
                            {
                                currentTokens.Add(token);
                                currentTokens.Add(";");                            
                            }   

                       
                            break;
                        }
                        case "rea":
                        {
                            if(currentTokens.Count > 1)
                            {
                                result = doArithmetic(currentTokens);

                                currentTokens.Clear();
                                currentTokens.Add(token);
                                currentTokens.Add(result.ToString());
                                currentTokens.Add(";");
                            }
                            else if(currentTokens.Count == 1)
                            {
                                currentTokens.Insert(0, token);
                                currentTokens.Add(";");                            
                            }
                            else
                            {
                                currentTokens.Add(token);
                                currentTokens.Add(";");                            
                            }   

                            break;
                        }       
                        case "inc":
                        {
                            if(currentTokens.Count > 1)
                            {
                                result = doArithmetic(currentTokens);

                                currentTokens.Clear();
                                currentTokens.Add(token);
                                currentTokens.Add(result.ToString());
                                currentTokens.Add(";");
                            }
                            else if(currentTokens.Count == 1)
                            {
                                currentTokens.Insert(0, token);
                                currentTokens.Add(";");                            
                            }
                            else
                            {
                                currentTokens.Add(token);
                                currentTokens.Add(";");                            
                            }   
                                
                            break;
                        }                                            
                        case "rov":
                        {
                            if(currentTokens.Count > 1)
                            {
                                result = doArithmetic(currentTokens);

                                currentTokens.Clear();
                                currentTokens.Add(token);
                                currentTokens.Add(result.ToString());
                                currentTokens.Add(";");
                            }
                            else if(currentTokens.Count == 1)
                            {
                                currentTokens.Insert(0, token);
                                currentTokens.Add(";");                            
                            }
                            else
                            {
                                currentTokens.Add(token);
                                currentTokens.Add(";");                            
                            }                        
                        
                            break;
                        }         
                        default:
                            currentTokens.Add(token);
                            currentTokens.Add(";");    
                            break;                                                                                                                                                                               
                    }      
                }

                line = "";
                for (int j = 0; j < currentTokens.Count; j++)
                {
                    line+=currentTokens.ElementAt(j);
                    if(j == 0 && line != "else" && line != "if")
                        line+=" "; 
                }
                attribute.Efectos.Add(line);
            }
        }   

        public void ExecuteEffects(CustomCard card, Player p1, Player p2)
        {
            int id = 0, id2 = 0, result, ifResult;
            bool posible = false;  
            bool exist = false;
            List<string> currentTokens;          
            for (int i = 0; i < card.Attribute.Count; i++)
            {
                if(card.Attribute[i].Lines.Count > 0)
                {
                    UpdateEffects(card.Attribute[i]);
                }
            }

            for (int i = 0; i < card.Attribute.Count; i++)
            {
                if(card.Attribute[i].Nombre == "efecto")
                {
                    for (int j = 0; j < card.Attribute[i].Efectos.Count; j++)
                    {
                        string lineEffect = card.Attribute[i].Efectos[j];

                        currentTokens = divideElements(lineEffect);  
                        string token = currentTokens.ElementAt(0);

                        if(token == "else")
                        {
                            if(exist == true)
                                j++;
                            continue;
                        }
                        if(token == "if")
                        {
                            currentTokens.RemoveAt(0);
                            currentTokens.RemoveAt(0);
                            currentTokens.RemoveAt(currentTokens.Count - 1);

                            result = doArithmetic(currentTokens.TakeWhile(x => conditionOperators.IndexOf(x) == -1).ToList<string>());

                            int cantidad = currentTokens.TakeWhile(x => conditionOperators.IndexOf(x) == -1).ToList<string>().Count;
                            ifResult = doArithmetic(currentTokens.TakeLast(currentTokens.Count-(cantidad+1)).ToList<string>());

                            string comparator = currentTokens.ElementAt(cantidad);

                            switch(comparator)
                            {
                                case ">":
                                {
                                    if(result > ifResult)
                                    {
                                        exist = true;
                                    }
                                    else
                                    {
                                        j++;
                                        exist = false;
                                    }
                                    continue;
                                }
                                case "<":
                                {
                                    if(result < ifResult)
                                    {
                                        exist = true;
                                    }
                                    else
                                    {
                                        j++;
                                        exist = false;
                                    }   
                                    continue;                         
                                }
                                case ">=":
                                {
                                    if(result >= ifResult)
                                    {
                                        exist = true;
                                    }
                                    else
                                    {
                                        j++;
                                        exist = false;
                                    }                
                                    continue;            
                                }
                                case "<=":
                                {
                                    if(result <= ifResult)
                                    {
                                        exist = true;
                                    }
                                    else
                                    {
                                        j++;
                                        exist = false;
                                    }                
                                    continue;   
                                }
                                case "==":
                                {
                                    if(result == ifResult)
                                    {
                                        exist = true;
                                    }
                                    else
                                    {
                                        j++;
                                        exist = false;
                                    }                
                                    continue;   
                                }
                                case "!=":
                                {
                                    if(result != ifResult)
                                    {
                                        exist = true;
                                    }
                                    else
                                    {
                                        j++;
                                        exist = false;
                                    }                
                                    continue;   
                                }                                                                                                                                                                       
                            }
                        }
                        else
                        {
                            lineEffect.Remove(lineEffect.Length - 1);                        

                            switch(token)
                            {
                                case "inv;":
                                {
                                    System.Console.WriteLine("Entre el id de la carta a invocar");
                                    id2 = Int32.Parse(System.Console.ReadLine());
                                    posible = Operations.InvocarEspecial(p1, id2,card);
                                    if(posible == false)
                                        System.Console.WriteLine("No puede realizar esa accion");                        
                                    break;
                                }
                                case "red;":
                                {
                                    if(card.Attribute[i].Efectos[j].Split(" ").ToArray().Length > 1)
                                        Operations.ReducirVida(p1,p2,card, Int32.Parse(card.Attribute[i].Efectos[j].Split(" ").ToArray()[1]));
                                    else    
                                        Operations.ReducirVida(p1,p2,card);                                     
                                    break;
                                }
                                case "rea;":
                                {
                                    System.Console.WriteLine("Entre el id de la carta a afectar");
                                    id2 = Int32.Parse(System.Console.ReadLine());
                                    if(p2.existCardInTheField(id2))
                                    {
                                        if(card.Attribute[i].Efectos[j].Split(" ").ToArray().Length > 1)
                                            posible = Operations.ReducirAtaque(p1,card,p2, id2, Int32.Parse(card.Attribute[i].Efectos[j].Split(" ").ToArray()[1]));
                                        else
                                            posible = Operations.ReducirAtaque(p1,card,p2, id2);
                                        if(posible == false)
                                            System.Console.WriteLine("No puede realizar esa accion");                             
                                    }
                                    else
                                        System.Console.WriteLine("No puede realizar esa accion"); 
                                    break;
                                }
                                case "atq;":
                                {
                                    System.Console.WriteLine("Entre el id de la carta a atacar");
                                    id2 = Int32.Parse(System.Console.ReadLine());
                                    if(p2.existCardInTheField(id2))
                                    {
                                        Operations.AtacarEspecial(p1, p2, p1.cardPositionInTheMonsterField(id), p2.cardPositionInTheMonsterField(id2));                            
                                    }                      
                                    else
                                        System.Console.WriteLine("No puede realizar esa accion");                                              
                                    break;
                                }
                                case "cam;":
                                {
                                    System.Console.WriteLine("Entre el id de la carta a afectar");
                                    id2 = Int32.Parse(System.Console.ReadLine());  
                                    if(p2.existCardInTheField(id2))
                                    {
                                    posible = Operations.Cambiar(p1,card,p2, id2);    
                                    if(posible == false)
                                            System.Console.WriteLine("No puede realizar esa accion");                   
                                    }       
                                    else
                                        System.Console.WriteLine("No puede realizar esa accion");                     
                                    break;
                                }   
                                case "roc;":
                                {
                                    Operations.Draw(p1,card ,1);                         
                                    break;
                                }           
                                case "rev;":
                                {
                                    System.Console.WriteLine("Entre el id de la carta a seleccionar");
                                    id2 = Int32.Parse(System.Console.ReadLine());
                                    if(p1.existCardInTheGraveyard(id2))
                                    {
                                    posible = Operations.Revivir(p1,card, id2);    
                                    if(posible == false)
                                            System.Console.WriteLine("No puede realizar esa accion");                   
                                    }     
                                    else
                                        System.Console.WriteLine("No puede realizar esa accion");                                                                                             
                                    break;
                                }        
                                case "con;":
                                {
                                    System.Console.WriteLine("Entre el id de la carta a controlar");
                                    id2 = Int32.Parse(System.Console.ReadLine());
                                    if(p2.existCardInTheField(id2))
                                    {
                                    posible = Operations.Controlar(p1,card, p2, id2);    
                                    if(posible == false)
                                            System.Console.WriteLine("No puede realizar esa accion");                   
                                    }     
                                    else
                                        System.Console.WriteLine("No puede realizar esa accion");                                                 
                                    break;
                                }       
                                case "inc;":
                                {
                                    if(card.Attribute[i].Efectos[j].Split(" ").ToArray().Length > 1)
                                        Operations.IncrementarAtaque(p1,card, id, Int32.Parse(card.Attribute[i].Efectos[j].Split(" ").ToArray()[1]));
                                    else    
                                        Operations.IncrementarAtaque(p1,card, id);                                                                 
                                    break;
                                }                                           
                                case "sub;":
                                {
                                    System.Console.WriteLine("Entre el id de la carta a subir");
                                    id2 = Int32.Parse(System.Console.ReadLine());
                                    if(p2.existCardInTheField(id2))
                                    {
                                    posible = Operations.Subir(p1,card,p2, id2);    
                                    if(posible == false)
                                            System.Console.WriteLine("No puede realizar esa accion");                   
                                    }     
                                    else
                                        System.Console.WriteLine("No puede realizar esa accion");                                                    
                                    break;
                                }            
                                case "dev;":
                                {
                                    
                                    break;
                                }   
                                case "rov;":
                                {                                       
                                    if(card.Attribute[i].Efectos[j].Split(" ").ToArray().Length > 1)
                                        Operations.RobarVida(p1,card, p2, Int32.Parse(card.Attribute[i].Efectos[j].Split(" ").ToArray()[1]));
                                    else    
                                        Operations.RobarVida(p1,card, p2);                                               
                                    break;
                                } 
                                case "des;":
                                {
                                    System.Console.WriteLine("Entre el id de la carta a destruir");
                                    id2 = Int32.Parse(System.Console.ReadLine());
                                    if(p2.existCardInTheField(id2))
                                    {
                                    posible = Operations.Destruir(p1,p2, id2,card);    
                                    if(posible == false)
                                            System.Console.WriteLine("No puede realizar esa accion");                   
                                    }     
                                    else
                                        System.Console.WriteLine("No puede realizar esa accion");                                               
                                    break;
                                }                                                                                                                                                          
                            }      
                        }
                    }
                }
            }            
        }     
    }
}