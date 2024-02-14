using System;
using System.Data.Entity;
using Jazb.DomainClasses;
using Jazb.DomainClasses.Entities;
using Jazb.DomainClasses.EntityConfiguration;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Jazb.Datalayer.Context
{
    public class JazbDbContext : DbContext, IUnitOfWork
    {

        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<Role> Roles { get; set; }
       
      
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageAnsware> MessageAnswares { get; set; }      
        public DbSet<UserMetaData> UserMetaDatas { get; set; }
        public DbSet<AnonymousUser> AnnonymousUsers { get; set; }    
        public DbSet<Option> Options { get; set; }
        public DbSet<ForgottenPassword> ForgottenPasswords { get; set; }

        public DbSet<AcceptResult> AcceptResults { get; set; }
        public DbSet<AttachmentImageSanjesh> AttachmentImageSanjeshs { get; set; }


        public DbSet<article> articles { get; set; }
        public DbSet<category> categories { get; set; }
        public DbSet<Azmoon> Azmoons { get; set; }
        public DbSet<Place> Places { get; set; }

        public DbSet<ErrorCode> ErrorCodes { get; set; }
        public DbSet<AutochthonType> AutochthonTypes { get; set; }
        public DbSet<AzmoonDegree> AzmoonDegrees { get; set; }
        public DbSet<AzmoonDevotionType> AzmoonDevotionTypes { get; set; }
        public DbSet<AzmoonDevotionValentear> AzmoonDevotionValentears { get; set; }
        public DbSet<AzmooneducatedSkill> AzmooneducatedSkills { get; set; }
        public DbSet<AzmoonJob> AzmoonJobs { get; set; }
        public DbSet<AzmoonPlace> AzmoonPlaces { get; set; }
        public DbSet<AzmoonQoutasValentear> AzmoonQoutasValentears { get; set; }
        public DbSet<AzmoonQoutaType> AzmoonQoutaTypes { get; set; }
        public DbSet<ConscriptStatus> ConscriptStatuses { get; set; }
        public DbSet<cooperationHistory> cooperationHistories { get; set; }
        public DbSet<degree> degrees { get; set; }
        public DbSet<DevotionType> DevotionTypes { get; set; }
        public DbSet<educatedSkill> educatedSkills { get; set; }
        public DbSet<Faith> Faiths { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<marriageStatus> marriageStatuses { get; set; }
        public DbSet<PlaneStatus> PlaneStatuses { get; set; }
        public DbSet<QoutaType> QoutaTypes { get; set; }
        public DbSet<Religion> Religions { get; set; }
        public DbSet<Valentear> Valentears { get; set; }

        public DbSet<AzmoonAnswerKey> AzmoonAnswerKeies { get; set; }
        public DbSet<AzmoonValentearAnswer> AzmoonValentearAnswers { get; set; }
        public DbSet<AzmoonValentearResult> AzmoonValentearResults { get; set; }

        public DbSet<AzmoonStatuse> AzmoonStatuses { get; set; }
        public DbSet<AmountCard> AmountCards { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<MassageList> MassageLists { get; set; }

        public DbSet<FAQ> FAQs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
         
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new MessageConfig());
            modelBuilder.Configurations.Add(new MessageAnswareConfig());           
            modelBuilder.Configurations.Add(new UserMetaDataConfig());
            modelBuilder.Configurations.Add(new AnonymousUserConfig());           
            modelBuilder.Configurations.Add(new OptionConfig());
            modelBuilder.Configurations.Add(new ForgottenPasswordConfig());

            base.OnModelCreating(modelBuilder);
        }

        #region IUnitOfWork Members

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
            
           
        }

        public void BulkInsertAllData<T>(IEnumerable<T> model) where T : class
        {
            this.BulkInsert(model);
        }

        public void BulkUpdateAllData<T>(IEnumerable<T> model) where T : class
        {
            this.BulkUpdate(model);
        }

       





        #endregion
    }
}