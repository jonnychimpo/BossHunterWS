using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using WebService5.REST.Relics;
using static WebService5.REST.Helper;

namespace WebService5.REST
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class BossHunter
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        private static List<String> lst = new List<String>(new String[] { "Arrays", "Queues", "Stacks" });
        private static List<int> StarValues = new List<int>(new int[] { 100, 200, 300, 400, 500, 600, 700, 800, 900 });


        public enum Stars
        {
            Wood,
            Rock,
            Bronze,
            Iron,
            StainlessSteel,
            Silver,
            Gold,
            Diamond,
            Onyx
        }

        [WebGet(UriTemplate = "/GetStar/{Score}")]
        public Stars GetStar(string Score)
        {
            int theScore;
            string TheStar = string.Empty;
            Int32.TryParse(Score, out theScore);
            return FindStar(theScore);
        }

        [WebGet(UriTemplate = "/GetStar/Random")]
        public string GetStarRandom()
        {
            string dText = string.Empty;
            Random newRand = new Random();
            Stars tempStar = new Stars();
            int WhichNum = newRand.Next(1000);

            tempStar = FindStar(WhichNum);

            dText = "Random: " + WhichNum + "=> Star Level: " + tempStar;

            return dText;
        }

        private Stars FindStar(int theScore)
        {
            Stars tempStar = new Stars();

            for (int i = 0; i < StarValues.Count; i++)
            {
                if (theScore > StarValues[StarValues.Count - 1]) return (Stars)StarValues.Count - 1;
                if (theScore < StarValues[i])
                {
                    tempStar = (Stars)i;
                    return tempStar;
                }
            }

            return tempStar;
        }

        [WebGet(UriTemplate = "/Tutorial", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        public String GetAllTutorials()
        {
            int count = lst.Count;
            String TutorialList = "";
            foreach (string item in lst)
            {
                TutorialList = TutorialList + item + ",";
            }

            return TutorialList;
        }

        //[WebGet(UriTemplate = "/CaseData", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        //public string ShowCaseData()
        //{
        //    int responseCode;
        //    string jsonGetData = "";
        //    //string test = string.Empty;
        //    int itemCount = 1;
        //    StringBuilder returnHTML = new StringBuilder("<TABLE>");
        //    StringBuilder test = new StringBuilder();

        //    responseCode = JTAzureTableRESTCommands.GetAllTables(storageName, sAS, string.Empty, out jsonGetData);

        //    //System.Console.WriteLine("Request response = {0}", responseCode);
        //    RootTableObject returnDataSet = JsonConvert.DeserializeObject<RootTableObject>(jsonGetData);
        //    AzureTable returnJSON = JsonConvert.DeserializeObject<AzureTable>(jsonGetData);
        //    //System.Console.WriteLine("JSON response ({0} tables):", returnDataSet.value.Count);
        //    //test += "JSON response (" + returnDataSet.value.Count + " tables):";
        //    returnHTML.Append("JSON response (" + returnDataSet.value.Count + " tables):");
        //    foreach (AzureTable item in returnDataSet.value)
        //    {
        //        //System.Console.WriteLine("{0} - {1}", itemCount, item.TableName);
        //        //test += itemCount + " - " + item.TableName;
        //        returnHTML.Append("<TR><TD>" + itemCount + " - " + item.TableName + "</TD></TR>");
        //        itemCount++;
        //    }

        //    returnHTML.Append("</TABLE>");
        //    return returnHTML.ToString();
        //}

        [WebGet(UriTemplate = "/StartGame", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        public int StartGame()
        {
            string jsonGetData = string.Empty;
            int responseCode;

            AzureTable NewTable = new AzureTable("BossHuntv1");
            responseCode = JTAzureTableRESTCommands.GetAllTables(storageName, sAS, "('" + NewTable.TableName + "')", out jsonGetData);
            if (responseCode == 7)
            {
                responseCode = JTAzureTableRESTCommands.CreateTable("POST", storageName, sAS, "Tables", JsonConvert.SerializeObject(NewTable), string.Empty, string.Empty);
                responseCode = JTAzureTableRESTCommands.GetAllTables(storageName, sAS, "('" + NewTable.TableName + "')", out jsonGetData);
            }

            if (responseCode == 200)
            {
                // If the Boss Hunt Table is successfully created or it exists, then check to see if there is an existing user and load, otherwise create the user
                IEntity defaultUser = new Hero(); // Create a Blank User
                //Entity blankUser = defaultUser.CreateBlankEntity(); // Create a Blank User
                defaultUser.CreateBlankEntity(); // Create a Blank User
                responseCode = JTAzureTableRESTCommands.GetAllEntities(storageName, sAS, NewTable.TableName + "(PartitionKey='" + defaultUser.GetEntityStat(EntityStat.PartitionKey) + "',RowKey='" + defaultUser.GetEntityStat(EntityStat.RowKey) + "')", out jsonGetData);
                string test = JsonConvert.SerializeObject(defaultUser);

                if (responseCode == 7)
                {
                    responseCode = JTAzureTableRESTCommands.ChangeEntity("POST", storageName, sAS, NewTable.TableName, JsonConvert.SerializeObject(defaultUser), string.Empty, string.Empty);
                }
                else
                {
                    responseCode = 201; // User Loaded or Created successfully.
                }

                // If the User is successfully created or already existed, then check to see if there is an existing boss and load, otherwise create the boss
                IEntity defaultBoss = new Boss(); // Create a Blank Boss
                //Entity blankBoss = defaultBoss.CreateBlankEntity();
                defaultBoss.CreateBlankEntity();
                responseCode = JTAzureTableRESTCommands.GetAllEntities(storageName, sAS, NewTable.TableName + "(PartitionKey='" + defaultBoss.GetEntityStat(EntityStat.PartitionKey) + "',RowKey='" + defaultBoss.GetEntityStat(EntityStat.RowKey) + "')", out jsonGetData);

                if (responseCode == 7)
                {
                    responseCode = JTAzureTableRESTCommands.ChangeEntity("POST", storageName, sAS, NewTable.TableName, JsonConvert.SerializeObject(defaultBoss), string.Empty, string.Empty);
                }
                else
                {
                    responseCode = 201; // Boss Loaded or Created successfully.
                }
            
                // ###########################################################################
                // Create Relic Entries if they doesn't exist
                if (responseCode == 201)
                {
                    responseCode = Helper.CreateRelics(NewTable.TableName);
                }
                // ###########################################################################
            }
            return responseCode;
        }

        [WebGet(UriTemplate = "/GetUserData/{TableName}/{PartitionK}/{RowK}", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        public string ReturnData(string TableName, string PartitionK, string RowK)
        {
            string jsonGetData = string.Empty;
            int responseCode;

            responseCode = JTAzureTableRESTCommands.GetEntityProperty("GET", storageName, sAS, TableName, out jsonGetData, PartitionK, RowK);

            //HeroEntity heroProperty = JsonConvert.DeserializeObject<HeroEntity>(jsonGetData);
            return jsonGetData;
        }

        [WebGet(UriTemplate = "/Attack/{TableName}/{UserName}/{BossName}", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        public string StartAttack(string TableName, string UserName, string BossName)
        {
            // Start the Attack
            // Entity with the highest speed goes first
            string jsonGetData = string.Empty;
            int responseCode;
            int DeadCode = 200; // This is used to determine if the User or Boss are dead.
            string PartitionKey = "Hero";
            StringBuilder theMessage = new StringBuilder();

            // Get User Data
            theMessage.Append("Loading User=>");
            responseCode = JTAzureTableRESTCommands.GetEntityProperty("GET", storageName, sAS, TableName, out jsonGetData, PartitionKey, UserName);

            //HeroEntity userHero = JsonConvert.DeserializeObject<HeroEntity>(jsonGetData);
            //userHero.AttackCount++; // Increment the attack count for this user on this boss.
            //userHero = Helper.CalculateScore(userHero);
            //AnyEntity genUser = new AnyEntity(userHero); // Convert to Basic hero entity for the Attacker Class

            // Factory Version
            IEntity genericUser = new Hero();
            genericUser.LoadEntity(jsonGetData);
            genericUser.ChangeEntityStat(EntityStat.AttackCount, ChangeStatDirection.Up, 1);
            genericUser.CalculateEntityScore();

            theMessage.Append(responseCode + newLine);

            // Get Boss Data
            theMessage.Append("Loading Boss=>");
            responseCode = JTAzureTableRESTCommands.GetEntityProperty("GET", storageName, sAS, TableName, out jsonGetData, PartitionKey, BossName);
            //BossEntity bossHero = JsonConvert.DeserializeObject<BossEntity>(jsonGetData);
            //AnyEntity genBoss = new AnyEntity(bossHero); // Convert to Basic hero entity for the Attacker Class

            // Factory Version
            IEntity genericBoss = new Boss();
            genericBoss.LoadEntity(jsonGetData);
            
            theMessage.Append(responseCode + newLine);

            //Start the Attack
            theMessage.Append("==Start Attack==" + newLine);
            //genUser.AttackCount++; // Increment the attack count for this user on this boss.
            Attack thisAttack = new Attack(genericUser, genericBoss, 0, 0);
            theMessage.Append("==User Goes First==" + newLine);

            // Determine who goes first
            if (Convert.ToInt32(genericUser.GetEntityStat(EntityStat.Speed)) < Convert.ToInt32(genericBoss.GetEntityStat(EntityStat.Speed)))
            {
                theMessage.Append("==Nope Boss Goes First==" + newLine);
                thisAttack.Attacker = genericBoss;
                thisAttack.Defender = genericUser;
            }

            // Check to see if the Attacker hit the Defender based on the Attackers Accuracy
            if (thisAttack.Attacker.CheckEntityHit() == 1)
            {
                // Attack was Successful
                // ###############################################################
                thisAttack.Defender.ChangeEntityStat(EntityStat.Health, ChangeStatDirection.Down, Convert.ToInt32(thisAttack.Attacker.GetEntityStat(EntityStat.Attack))); // Lower Health
                theMessage.Append(thisAttack.Attacker.GetEntityStat(EntityStat.RowKey) + " hits " + thisAttack.Defender.GetEntityStat(EntityStat.RowKey) + " for " + thisAttack.Attacker.GetEntityStat(EntityStat.Attack) + " damage" + newLine);

                if (thisAttack.Defender.GetEntityStat(EntityStat.RowKey) == BossName) // Check Boss Threshold
                {
                    // Get the current Boss threshold Value. Increase the speed based on this new value.
                    thisAttack.Defender.ChangeEntityStat(EntityStat.CurrentThreshold, ChangeStatDirection.Set, Convert.ToInt32(thisAttack.Defender.GetEntityThresholdValue(10)));
                    thisAttack.Defender.ChangeEntityStat(EntityStat.Speed, ChangeStatDirection.Up, Convert.ToInt32(thisAttack.Defender.GetEntityStat(EntityStat.CurrentThreshold)));
                }

                if (thisAttack.Attacker.GetEntityStat(EntityStat.RowKey) == UserName)
                {
                    // If the attack was done by the User, Increase the XP of the User.
                    thisAttack.Attacker.ChangeEntityStat(EntityStat.XP, ChangeStatDirection.Up, Convert.ToInt32(thisAttack.Attacker.GetEntityStat(EntityStat.Attack)));
                }

                // Check if the Defender Died during this attack. If so, end current Attack
                if (Convert.ToInt32(thisAttack.Defender.GetEntityStat(EntityStat.Health)) < 1)
                {
                    thisAttack.DefenderDead = 1;
                    theMessage.Append(thisAttack.Defender.GetEntityStat(EntityStat.RowKey) + " as Defender Died" + newLine);
                    if (thisAttack.Defender.GetEntityStat(EntityStat.RowKey) == UserName)
                    {
                        DeadCode = 201; // Means the User died
                    }
                    else
                    {
                        DeadCode = 202; // Means the Boss died
                    }
                }

                // Update the Defender Health
                theMessage.Append("Saving Defender Stats=>");
                responseCode = JTAzureTableRESTCommands.ChangeEntity("PUT", storageName, sAS, TableName, JsonConvert.SerializeObject(thisAttack.Defender), thisAttack.Defender.GetEntityStat(EntityStat.PartitionKey), thisAttack.Defender.GetEntityStat(EntityStat.RowKey));
                theMessage.Append(responseCode + newLine);
                // ###############################################################
            }
            else
            {
                // Attack failed
                // ###############################################################
                theMessage.Append("ATTACK MISSED" + newLine);
                // ###############################################################
            }

            if (thisAttack.DefenderDead == 0) // Check if the defender is still Alive, if so Defenders turn to Attack
            {
                theMessage.Append("==Defender NOT Dead==" + newLine);
                thisAttack = Helper.SwitchRoles(thisAttack, thisAttack.Attacker, thisAttack.Defender);
                theMessage.Append("==Switch Roles==" + newLine);

                // Check to see if the Attacker hit the Defender based on the Attackers Accuracy
                if (thisAttack.Attacker.CheckEntityHit() == 1)
                {
                    // Attack was Successful
                    // ###############################################################
                    thisAttack.Defender.ChangeEntityStat(EntityStat.Health, ChangeStatDirection.Down, Convert.ToInt32(thisAttack.Attacker.GetEntityStat(EntityStat.Attack)));
                    theMessage.Append(thisAttack.Attacker.GetEntityStat(EntityStat.RowKey) + " hits " + thisAttack.Defender.GetEntityStat(EntityStat.RowKey) + " for " + thisAttack.Attacker.GetEntityStat(EntityStat.Attack) + " damage" + newLine);

                    if (thisAttack.Defender.GetEntityStat(EntityStat.RowKey) == BossName) // Check Boss Threshold
                    {
                        // Get the current Boss threshold Value. Increase the speed based on this new value.
                        thisAttack.Defender.ChangeEntityStat(EntityStat.CurrentThreshold, ChangeStatDirection.Set, Convert.ToInt32(thisAttack.Defender.GetEntityThresholdValue(10)));
                        thisAttack.Defender.ChangeEntityStat(EntityStat.Speed, ChangeStatDirection.Up, Convert.ToInt32(thisAttack.Defender.GetEntityStat(EntityStat.CurrentThreshold)));
                    }

                    if (thisAttack.Attacker.GetEntityStat(EntityStat.RowKey) == UserName)
                    {
                        // If the attack was done by the User, Increase the XP of the User.
                        thisAttack.Attacker.ChangeEntityStat(EntityStat.XP, ChangeStatDirection.Up, Convert.ToInt32(thisAttack.Attacker.GetEntityStat(EntityStat.Attack)));
                    }

                    if (Convert.ToInt32(thisAttack.Defender.GetEntityStat(EntityStat.Health)) < 1)
                    {
                        thisAttack.DefenderDead = 1;
                        theMessage.Append(thisAttack.Defender.GetEntityStat(EntityStat.RowKey) + " as Defender Died" + newLine);
                        if (thisAttack.Defender.GetEntityStat(EntityStat.RowKey) == UserName)
                        {
                            DeadCode = 201; // Means the User died
                        }
                        else
                        {
                            DeadCode = 202; // Means the Boss died
                        }
                    }

                    // Update the new Defender Health
                    theMessage.Append("Saving Defender Stats=>");
                    responseCode = JTAzureTableRESTCommands.ChangeEntity("PUT", storageName, sAS, TableName, JsonConvert.SerializeObject(thisAttack.Defender), thisAttack.Defender.GetEntityStat(EntityStat.PartitionKey), thisAttack.Defender.GetEntityStat(EntityStat.RowKey));
                    theMessage.Append(responseCode + newLine);
                    // ###############################################################
                }
                else
                {
                    // Attack failed
                    // ###############################################################
                    theMessage.Append("ATTACK MISSED" + newLine);
                    // ###############################################################
                }
            }
            else
            {
                theMessage.Append("==Defender Dead==" + newLine);
                responseCode = JTAzureTableRESTCommands.ChangeEntity("PUT", storageName, sAS, TableName, JsonConvert.SerializeObject(thisAttack.Attacker), thisAttack.Attacker.GetEntityStat(EntityStat.PartitionKey), thisAttack.Attacker.GetEntityStat(EntityStat.RowKey));
            }

            theMessage.Append("==Attack Over==");

            ResponseMessage response = new ResponseMessage(theMessage.ToString(), DeadCode);

            return JsonConvert.SerializeObject(response);
        }

        [WebGet(UriTemplate = "/ResetGame/{TableName}/{UserName}/{BossName}", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        public string ResetGame(string TableName, string UserName, string BossName)
        {
            string jsonGetData = string.Empty;
            int responseCode;
            StringBuilder theMessage = new StringBuilder();

            // Reset User
            theMessage.Append("Resetting User=>");
            IEntity defaultUser = new Hero(); // Create a Blank User
            defaultUser.CreateBlankEntity();
            responseCode = JTAzureTableRESTCommands.ChangeEntity("PUT", storageName, sAS, TableName, JsonConvert.SerializeObject(defaultUser), defaultUser.GetEntityStat(EntityStat.PartitionKey), defaultUser.GetEntityStat(EntityStat.RowKey));
            theMessage.Append(responseCode + newLine);

            // Reset Boss
            theMessage.Append("Resetting Boss=>");
            IEntity defaultBoss = new Boss(); // Create a Blank Boss
            defaultBoss.CreateBlankEntity();
            responseCode = JTAzureTableRESTCommands.ChangeEntity("PUT", storageName, sAS, TableName, JsonConvert.SerializeObject(defaultBoss), defaultBoss.GetEntityStat(EntityStat.PartitionKey), defaultBoss.GetEntityStat(EntityStat.RowKey));
            theMessage.Append(responseCode + newLine);

            responseCode = 200; // Reset Complete

            ResponseMessage response = new ResponseMessage(theMessage.ToString(), responseCode);
            return JsonConvert.SerializeObject(response);
        }

        [WebGet(UriTemplate = "/Respawn/{TableName}/{UserName}", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        public string Respawn(string TableName, string UserName)
        {
            string jsonGetData = string.Empty;
            int responseCode;
            StringBuilder theMessage = new StringBuilder();

            // Reset User
            theMessage.Append("Respawn User=>");
            responseCode = JTAzureTableRESTCommands.GetEntityProperty("GET", storageName, sAS, TableName, out jsonGetData, "Hero", UserName);
            // Factory Version
            IEntity loadUser = new Hero();
            loadUser.LoadEntity(jsonGetData);
            loadUser.ChangeEntityStat(EntityStat.RespawnCount, ChangeStatDirection.Up, 1);
            loadUser.ChangeEntityStat(EntityStat.Health, ChangeStatDirection.Set, Convert.ToInt32(loadUser.GetEntityStat(EntityStat.Health)) + Convert.ToInt32(loadUser.GetEntityStat(EntityStat.AddedHealth)));
            loadUser.CalculateEntityScore();

            responseCode = JTAzureTableRESTCommands.ChangeEntity("PUT", storageName, sAS, TableName, JsonConvert.SerializeObject(loadUser), loadUser.GetEntityStat(EntityStat.PartitionKey), loadUser.GetEntityStat(EntityStat.RowKey));
            theMessage.Append(responseCode + newLine);

            responseCode = 200; // Reset Complete

            ResponseMessage response = new ResponseMessage(theMessage.ToString(), responseCode);
            return JsonConvert.SerializeObject(response);
        }

        [WebGet(UriTemplate = "/IncreaseStat/{TableName}/{UserName}/{Relic}", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        public string IncreaseStat(string TableName, string UserName, string Relic)
        {
            string jsonGetData = string.Empty;
            int responseCode;
            StringBuilder theMessage = new StringBuilder();

            // User clicked on a "Relic" which will increase the stat based on the Relic selected.
            responseCode = JTAzureTableRESTCommands.GetEntityProperty("GET", storageName, sAS, TableName, out jsonGetData, "Hero", UserName);
            IEntity loadUser = new Hero();
            loadUser.LoadEntity(jsonGetData);

            // Make sure User is not dead
            if (Convert.ToInt32(loadUser.GetEntityStat(EntityStat.Health)) > 0)
            {
                theMessage.Append("Increasing " + Relic + " =>");
                loadUser.ChangeEntityStatByRelic(Relic);
                responseCode = JTAzureTableRESTCommands.ChangeEntity("PUT", storageName, sAS, TableName, JsonConvert.SerializeObject(loadUser), loadUser.GetEntityStat(EntityStat.PartitionKey), loadUser.GetEntityStat(EntityStat.RowKey));
                theMessage.Append(responseCode + newLine);

                responseCode = 200; // Stat Increase Complete
            }
            else
            {
                theMessage.Append("User is DEAD. Respawning now =>");
                responseCode = 498; // User is dead. Need to Respawn
            }
            ResponseMessage response = new ResponseMessage(theMessage.ToString(), responseCode);
            return JsonConvert.SerializeObject(response);
        }

        [WebGet(UriTemplate = "/Leaderboard/{TableName}", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        public string GetLeaderboard(string TableName)
        {
            string jsonGetData = string.Empty;
            int responseCode;
            StringBuilder theMessage = new StringBuilder();
            RootLeaderBoardObject theLB = new RootLeaderBoardObject();

            // Get LeaderBoard
            responseCode = JTAzureTableRESTCommands.GetLBProperty("GET", storageName, sAS, TableName, out jsonGetData, "Leaderboard");
            theLB = JsonConvert.DeserializeObject<RootLeaderBoardObject>(jsonGetData);

            if (theLB.value != null)
            {
                // Sort Leaderboard
                Sorter sort = new Sorter();
                theLB.value.Sort(sort);
                //theLB.value.Reverse();
                return JsonConvert.SerializeObject(theLB);
            }
            else
            {
                return "NoData";
            }
        }

        [WebGet(UriTemplate = "/Leaderboard/{TableName}/{User2Add}", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        public string Add2Leaderboard(string TableName, string User2Add)
        {
            string jsonGetData = string.Empty;
            int responseCode;
            StringBuilder theMessage = new StringBuilder();

            // Add user to Leaderboard
            theMessage.Append("Adding User " + User2Add + " to LeaderBoard");
            responseCode = JTAzureTableRESTCommands.GetEntityProperty("GET", storageName, sAS, TableName, out jsonGetData, "Hero", "User");
            HeroEntity userHero = JsonConvert.DeserializeObject<HeroEntity>(jsonGetData);
            LeaderBoardEntry newEntry = new LeaderBoardEntry();
            newEntry.RowKey = User2Add;
            newEntry.AttackCount = userHero.AttackCount;
            newEntry.RespawnCount = userHero.RespawnCount;
            newEntry.Score = userHero.Score;
            newEntry.AttackDate = DateTime.Now.ToShortDateString();

            // Create new Leaderboard Entry
            responseCode = JTAzureTableRESTCommands.ChangeEntity("POST", storageName, sAS, TableName, JsonConvert.SerializeObject(newEntry), string.Empty, string.Empty);
            theMessage.Append(responseCode + newLine);

            responseCode = 200; // Leaderboard Entry Added

            ResponseMessage response = new ResponseMessage(theMessage.ToString(), responseCode);
            return JsonConvert.SerializeObject(response);
        }

        [WebGet(UriTemplate = "/GetRelicData/{TableName}", ResponseFormat = System.ServiceModel.Web.WebMessageFormat.Json)]
        public string ReturnRelicData(string TableName)
        {
            string jsonGetData = string.Empty;
            int responseCode;
            StringBuilder theMessage = new StringBuilder();
            RootRelicObject RelicList = new RootRelicObject();

            // Get LeaderBoard
            responseCode = JTAzureTableRESTCommands.GetLBProperty("GET", storageName, sAS, TableName, out jsonGetData, "Relic");
            RelicList = JsonConvert.DeserializeObject<RootRelicObject>(jsonGetData);

            if (RelicList.value != null)
            {
                // return Relic List
                //RelicList.value.Sort(sort);
                return JsonConvert.SerializeObject(RelicList);
            }
            else
            {
                return "NoData";
            }
        }

        //[WebGet(UriTemplate = "/Tutorial/Loop/{LoopID}")]
        //public String GetAllTutorialsLoop(string LoopID)
        //{
        //    int count = lst.Count;
        //    String TutorialList = "";

        //    int LID;
        //    Int32.TryParse(LoopID, out LID);

        //    if (LID == 1)
        //    {
        //        TutorialList += "Using For Loop: ";
        //        foreach (string item in lst)
        //        {
        //            TutorialList = TutorialList + item + ",";
        //        }
        //    } else if (LID == 2)
        //    {
        //        TutorialList += "Using ForEach Loop: ";
        //        for (int i = 0; i < count; i++)
        //        {
        //            TutorialList = TutorialList + lst[i] + ",";
        //        }
        //    }
        //    return TutorialList;
        //}

        //[WebGet(UriTemplate = "/Tutorial/{Tutorialid}")]
        //public String GetTutorialbyID(String Tutorialid)
        //{
        //    int pid;
        //    Int32.TryParse(Tutorialid, out pid);
        //    return lst[pid];
        //}

        //[WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, UriTemplate = "/Tutorial", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //public void AddTutorial(String str)
        //{
        //    lst.Add(str);
        //}

        //[WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, UriTemplate = "/Tutorial/{Tutorialid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //public void DeleteTutorial(String Tutorialid)
        //{
        //    int pid;
        //    Int32.TryParse(Tutorialid, out pid);
        //    lst.RemoveAt(pid);
        //}


        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
