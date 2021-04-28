using Md_exercise.Log4net;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Md_exercise.Core.Services.Hosted_Services
{
    public class RefreshTrainingAmountHostedService : BackgroundService
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
        IConfiguration configuration;
        const int TRAINING_AMOUNT = 0;
        const int MAX_ATTEMPTS_AMOUNT = 3;
        int attemptsMade = 0;
        public RefreshTrainingAmountHostedService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected async override Task ExecuteAsync(CancellationToken token)
        {
            log.Info("Initializing Hero training amount refresh");
           
            while (!token.IsCancellationRequested)
            {
                TimeSpan Midnight = TimeSpan.FromHours(24 - DateTime.Now.TimeOfDay.TotalHours);
                await Task.Delay(Midnight, token);
                this.refreshTrainingAmount(TRAINING_AMOUNT);
            }
        }

        private void refreshTrainingAmount(int trainingAmount)
        {
            attemptsMade++;
            try
            {
                using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("HeroesDbConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("RefreshTrainingAmount", conn))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TrainingAmount", trainingAmount);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        attemptsMade = 0;
                        log.Info($"training amount was set to {trainingAmount}");
                    }
                }
            }catch(Exception e)
            {
                log.Error("training amount refresh failed",e);
                if (attemptsMade < MAX_ATTEMPTS_AMOUNT)
                {     
                    refreshTrainingAmount(trainingAmount);
                }
            }
        }
    }
   



}

