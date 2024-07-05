using MonoMod.RuntimeDetour.HookGen;
using System;
using UnityEngine;

namespace NoRestForTheWicked___Infinite_Week_Days.Patches
{
    public class NextDayPatch
    {
        /// <summary>
        /// This method is called when the game is started to initialize the patch.
        /// </summary>
        public static void Init()
        {
            HookEndpointManager.Modify(typeof(RoomStatsHolder).GetMethod("NextDay"),
                                       new Action<RoomStatsHolder>(NextDayPatched));
        }

        /// <summary>
        /// This will replace the NextDay method by not calling the game over.
        /// Days will just continues like nothing wrong happened.
        /// </summary>
        /// <param name="self"></param>
        private static void NextDayPatched(RoomStatsHolder self)
        {
            Debug.Log("A\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\nA\n");
            self.ReceivedQuota = false;

            self.RegenerateSeed();
            self.GenerateBudget();

            if (self.m_QuotaDay) //TODO RESTORE THAT LINE
            {
                Debug.Log("B\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\nB\n");
                if (!self.CalculateIfReachedQuota())
                {
                    Debug.Log("C\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\nC\n");
                    Debug.Log("Did Not Reach Quota, Plz Die");
                    Debug.Log("NO DEVS! The player are under Skulls23's protection, I won´t allow you to kill them!");

                    //self.m_SurfaceHandler.FailedQuota(); //This is the call causing a game over

                    //I t could maybe cause maybe an overlap in the UI
                    //It's possible to get "day 8 out of 3" but game UI is designed like that.
                    //If it really bother people I could change UI to a "day 8 out of ?"

                    self.CurrentDay++;
                    self.CurrentQuotaDay++;


                    //We do the end of the method to avoid making the game waiting forever
                    self.OnStatsUpdated();

                    if (self.CurrentDay > 1)
                        SaveSystem.SaveToDisk();

                    return;
                }

                self.ResetCurrentQuotaDay();
                self.NewMapToPlay();

                self.CurrentDay++;
                self.CurrentQuotaDay++;

                Debug.Log("Reached Quota of: " + self.QuotaToReach + " Team Quota: " + self.CurrentQuota);

                self.CalculateNewQuota();
                self.m_SurfaceHandler.NewWeek(self.CurrentRun);
            }
            else
            {
                self.CurrentDay++;
                self.CurrentQuotaDay++;
            }

            Debug.Log("NEW DAY: " + self.CurrentDay + " Current QuotaDay: " + self.CurrentQuotaDay + "Days Left To Complete Quota Of: " + self.QuotaToReach + " : " + (self.DaysPerQutoa - self.CurrentQuotaDay));
            
            self.OnStatsUpdated();

            if (self.CurrentDay > 1)
                SaveSystem.SaveToDisk();
        }
    }
}
