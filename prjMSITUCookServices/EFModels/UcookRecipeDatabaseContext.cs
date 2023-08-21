using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prjMSITUCookServices.EFModels;

public partial class UcookRecipeDatabaseContext : DbContext
{
    public UcookRecipeDatabaseContext()
    {
    }

    public UcookRecipeDatabaseContext(DbContextOptions<UcookRecipeDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Follow追蹤> Follow追蹤s { get; set; }


    public virtual DbSet<Food食材> Food食材s { get; set; }

    public virtual DbSet<LatestLikeLog最新按讚紀錄> LatestLikeLog最新按讚紀錄s { get; set; }

    public virtual DbSet<MemberVerifyCode會員註冊驗證> MemberVerifyCode會員註冊驗證s { get; set; }

    public virtual DbSet<Member會員> Member會員s { get; set; }

    public virtual DbSet<NotificationRecord通知紀錄> NotificationRecord通知紀錄s { get; set; }

    public virtual DbSet<NotificationType通知類型> NotificationType通知類型s { get; set; }

    public virtual DbSet<PasswordSalt密碼鹽> PasswordSalt密碼鹽s { get; set; }

    public virtual DbSet<Recipe食譜> Recipe食譜s { get; set; }

    public virtual DbSet<Step步驟列表> Step步驟列表s { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=UCookRecipeDatabase;Integrated Security=True;TrustServerCertificate=true;\n");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Follow追蹤>(entity =>
        {
            entity.ToTable("FOLLOW_追蹤");

            entity.Property(e => e.Follow追蹤Id).HasColumnName("FOLLOW_追蹤_ID");
            entity.Property(e => e.FollowTime開始追蹤日期)
                .HasColumnType("datetime")
                .HasColumnName("FOLLOW_TIME開始追蹤日期");
            entity.Property(e => e.FollowedMemberId被追蹤者Fk).HasColumnName("FOLLOWED_MEMBER_ID被追蹤者_FK");
            entity.Property(e => e.FollowerMemberId追蹤者Fk).HasColumnName("FOLLOWER_MEMBER_ID追蹤者_FK");

            entity.HasOne(d => d.FollowedMemberId被追蹤者FkNavigation).WithMany(p => p.Follow追蹤FollowedMemberId被追蹤者FkNavigations)
                .HasForeignKey(d => d.FollowedMemberId被追蹤者Fk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FOLLOW_追蹤_MEMBER_會員1");

            entity.HasOne(d => d.FollowerMemberId追蹤者FkNavigation).WithMany(p => p.Follow追蹤FollowerMemberId追蹤者FkNavigations)
                .HasForeignKey(d => d.FollowerMemberId追蹤者Fk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FOLLOW_追蹤_MEMBER_會員");
        });

       

        modelBuilder.Entity<Food食材>(entity =>
        {
            entity.ToTable("FOOD_食材");

            entity.Property(e => e.Food食材Id).HasColumnName("FOOD_食材_ID");
            entity.Property(e => e.FoodAmount食材數量)
                .HasMaxLength(200)
                .HasColumnName("FOOD_AMOUNT食材數量");
            entity.Property(e => e.FoodName食材名稱)
                .HasMaxLength(200)
                .HasColumnName("FOOD_NAME食材名稱");
            entity.Property(e => e.RecipeFood食譜Fk).HasColumnName("RECIPE_FOOD_食譜_FK");

            entity.HasOne(d => d.RecipeFood食譜FkNavigation).WithMany(p => p.Food食材s)
                .HasForeignKey(d => d.RecipeFood食譜Fk)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_FOOD_食材_RECIPE_食譜");
        });

        modelBuilder.Entity<LatestLikeLog最新按讚紀錄>(entity =>
        {
            entity.HasKey(e => e.LatestLikeLog最新按讚紀錄Pk);

            entity.ToTable("LATEST_LIKE_LOG_最新按讚紀錄");

            entity.Property(e => e.LatestLikeLog最新按讚紀錄Pk).HasColumnName("LATEST_LIKE_LOG_最新按讚紀錄_PK");
            entity.Property(e => e.LikedRecipe按讚食譜Fk).HasColumnName("LIKED_RECIPE按讚食譜_FK");
            entity.Property(e => e.LikedTime按讚時間)
                .HasColumnType("datetime")
                .HasColumnName("LIKED_TIME按讚時間");
            entity.Property(e => e.MemberId會員Fk).HasColumnName("MEMBER_ID會員_FK");

            entity.HasOne(d => d.LikedRecipe按讚食譜FkNavigation).WithMany(p => p.LatestLikeLog最新按讚紀錄s)
                .HasForeignKey(d => d.LikedRecipe按讚食譜Fk)
                .HasConstraintName("FK_LATEST_LIKE_LOG_最新按讚紀錄_RECIPE_食譜");

            entity.HasOne(d => d.MemberId會員FkNavigation).WithMany(p => p.LatestLikeLog最新按讚紀錄s)
                .HasForeignKey(d => d.MemberId會員Fk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LATEST_LIKE_LOG_最新按讚紀錄_MEMBER_會員");
        });

        modelBuilder.Entity<MemberVerifyCode會員註冊驗證>(entity =>
        {
            entity.HasKey(e => e.MemberVerifyCodeId);

            entity.ToTable("MEMBER_VERIFY_CODE_會員註冊驗證");

            entity.Property(e => e.MemberVerifyCodeId).HasColumnName("MEMBER_VERIFY_CODE_ID");
            entity.Property(e => e.MemberId會員Fk).HasColumnName("MEMBER_ID會員_FK");
            entity.Property(e => e.VerifyCode)
                .HasMaxLength(100)
                .HasColumnName("VERIFY_CODE");
            entity.Property(e => e.VerityExpirationData過期日)
                .HasColumnType("datetime")
                .HasColumnName("VERITY_EXPIRATION_DATA_過期日");

            entity.HasOne(d => d.MemberId會員FkNavigation).WithMany(p => p.MemberVerifyCode會員註冊驗證s)
                .HasForeignKey(d => d.MemberId會員Fk)
                .HasConstraintName("FK_MEMBER_VERIFY_CODE_會員註冊驗證_MEMBER_會員");
        });

        modelBuilder.Entity<Member會員>(entity =>
        {
            entity.HasKey(e => e.MemberId會員Pk);

            entity.ToTable("MEMBER_會員");

            entity.Property(e => e.MemberId會員Pk).HasColumnName("MEMBER_ID會員_PK");
            entity.Property(e => e.Birthday生日)
                .HasColumnType("datetime")
                .HasColumnName("BIRTHDAY生日");
            entity.Property(e => e.Gender性別)
                .HasMaxLength(10)
                .HasColumnName("GENDER性別");
            entity.Property(e => e.MemberEmail).HasColumnName("MEMBER_EMAIL");
            entity.Property(e => e.MemberPassword).HasColumnName("MEMBER_PASSWORD");
            entity.Property(e => e.NickName暱稱)
                .HasMaxLength(40)
                .HasColumnName("NICK_NAME暱稱");
            entity.Property(e => e.ProfilePhoto頭貼).HasColumnName("PROFILE_PHOTO頭貼");
            entity.Property(e => e.ReceivedPersonAddress收件人地址)
                .HasMaxLength(200)
                .HasColumnName("RECEIVED_PERSON_ADDRESS收件人地址");
            entity.Property(e => e.ReceivedPersonName收件人姓名)
                .HasMaxLength(200)
                .HasColumnName("RECEIVED_PERSON_NAME收件人姓名");
            entity.Property(e => e.ReceivedPersonPhone收件人電話)
                .HasMaxLength(200)
                .HasColumnName("RECEIVED_PERSON_PHONE收件人電話");
            entity.Property(e => e.RegisterTime註冊時間)
                .HasColumnType("date")
                .HasColumnName("REGISTER_TIME註冊時間");
            entity.Property(e => e.SelfIntro自介)
                .HasMaxLength(400)
                .HasColumnName("SELF_INTRO自介");
        });

        modelBuilder.Entity<NotificationRecord通知紀錄>(entity =>
        {
            entity.HasKey(e => e.NotificationRecord通知紀錄Pk);

            entity.ToTable("NOTIFICATION_RECORD_通知紀錄");

            entity.Property(e => e.NotificationRecord通知紀錄Pk).HasColumnName("NOTIFICATION_RECORD_通知紀錄_PK");
            entity.Property(e => e.LinkedMemberId相關會員Fk).HasColumnName("LINKED_MEMBER_ID相關會員_FK");
            entity.Property(e => e.LinkedRecipe相關食譜Fk).HasColumnName("LINKED_RECIPE相關食譜_FK");
            entity.Property(e => e.MemberId會員Fk).HasColumnName("MEMBER_ID會員_FK");
            entity.Property(e => e.NotificationType通知類型編號).HasColumnName("NOTIFICATION_TYPE通知類型編號");
            entity.Property(e => e.NotifyTime通知時間)
                .HasColumnType("datetime")
                .HasColumnName("NOTIFY_TIME通知時間");
            entity.Property(e => e.Readed已讀時間)
                .HasColumnType("datetime")
                .HasColumnName("READED_已讀時間");

            entity.HasOne(d => d.LinkedMemberId相關會員FkNavigation).WithMany(p => p.NotificationRecord通知紀錄LinkedMemberId相關會員FkNavigations)
                .HasForeignKey(d => d.LinkedMemberId相關會員Fk)
                .HasConstraintName("FK_NOTIFICATION_RECORD_通知紀錄_MEMBER_會員1");

            entity.HasOne(d => d.LinkedRecipe相關食譜FkNavigation).WithMany(p => p.NotificationRecord通知紀錄s)
                .HasForeignKey(d => d.LinkedRecipe相關食譜Fk)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_NOTIFICATION_RECORD_通知紀錄_RECIPE_食譜");

            entity.HasOne(d => d.MemberId會員FkNavigation).WithMany(p => p.NotificationRecord通知紀錄MemberId會員FkNavigations)
                .HasForeignKey(d => d.MemberId會員Fk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTIFICATION_RECORD_通知紀錄_MEMBER_會員");

            entity.HasOne(d => d.NotificationType通知類型編號Navigation).WithMany(p => p.NotificationRecord通知紀錄s)
                .HasForeignKey(d => d.NotificationType通知類型編號)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NOTIFICATION_RECORD_通知紀錄_NOTIFICATION_TYPE_通知類型");
        });

        modelBuilder.Entity<NotificationType通知類型>(entity =>
        {
            entity.HasKey(e => e.NotificationType通知類型編號Pk);

            entity.ToTable("NOTIFICATION_TYPE_通知類型");

            entity.Property(e => e.NotificationType通知類型編號Pk).HasColumnName("NOTIFICATION_TYPE_通知類型編號_PK");
            entity.Property(e => e.NotificationTrigger通知時機).HasColumnName("NOTIFICATION_TRIGGER通知時機");
            entity.Property(e => e.NotificationTypeName通知類型名稱)
                .HasMaxLength(200)
                .HasColumnName("NOTIFICATION_TYPE_NAME通知類型名稱");
        });

        modelBuilder.Entity<PasswordSalt密碼鹽>(entity =>
        {
            entity.ToTable("PASSWORD_SALT密碼鹽");

            entity.Property(e => e.PasswordSalt密碼鹽Id).HasColumnName("PASSWORD_SALT密碼鹽_ID");
            entity.Property(e => e.MemberId會員Pk).HasColumnName("MEMBER_ID會員_PK");
            entity.Property(e => e.Salt)
                .HasMaxLength(100)
                .HasColumnName("SALT");

            entity.HasOne(d => d.MemberId會員PkNavigation).WithMany(p => p.PasswordSalt密碼鹽s)
                .HasForeignKey(d => d.MemberId會員Pk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PASSWORD_SALT密碼鹽_MEMBER_會員");
        });

        modelBuilder.Entity<Recipe食譜>(entity =>
        {
            entity.HasKey(e => e.Recipe食譜Pk);

            entity.ToTable("RECIPE_食譜");

            entity.Property(e => e.Recipe食譜Pk).HasColumnName("RECIPE食譜_PK");
            entity.Property(e => e.Amount份量)
                .HasMaxLength(20)
                .HasColumnName("AMOUNT_份量");
            entity.Property(e => e.Author作者).HasColumnName("AUTHOR_作者");
            entity.Property(e => e.CostMinutes花費時間)
                .HasMaxLength(20)
                .HasColumnName("COST_MINUTES花費時間");
            entity.Property(e => e.FavoriteNumber收藏數).HasColumnName("FAVORITE_NUMBER收藏數");
            entity.Property(e => e.Likes讚數).HasColumnName("LIKES_讚數");
            entity.Property(e => e.PublishedTime出版時間)
                .HasColumnType("datetime")
                .HasColumnName("PUBLISHED_TIME出版時間");
            entity.Property(e => e.RecipeCover).HasColumnName("RECIPE_COVER");
            entity.Property(e => e.RecipeName食譜名稱)
                .HasMaxLength(40)
                .HasColumnName("RECIPE_NAME食譜名稱");
            entity.Property(e => e.ShortDescription簡短說明)
                .HasMaxLength(400)
                .HasColumnName("SHORT_DESCRIPTION簡短說明");
            entity.Property(e => e.Views瀏覽數).HasColumnName("VIEWS_瀏覽數");

            entity.HasOne(d => d.Author作者Navigation).WithMany(p => p.Recipe食譜s)
                .HasForeignKey(d => d.Author作者)
                .HasConstraintName("FK_RECIPE_食譜_MEMBER_會員");
        });

        modelBuilder.Entity<Step步驟列表>(entity =>
        {
            entity.HasKey(e => e.Step步驟Id);

            entity.ToTable("STEP_步驟列表");

            entity.Property(e => e.Step步驟Id).HasColumnName("STEP_步驟_ID");
            entity.Property(e => e.RecipeStep食譜Fk).HasColumnName("RECIPE_STEP_食譜_FK");
            entity.Property(e => e.StepDescriptionPicture步驟附圖).HasColumnName("STEP_DESCRIPTION_PICTURE步驟附圖");
            entity.Property(e => e.StepDescription步驟說明)
                .HasMaxLength(400)
                .HasColumnName("STEP_DESCRIPTION步驟說明");

            entity.HasOne(d => d.RecipeStep食譜FkNavigation).WithMany(p => p.Step步驟列表s)
                .HasForeignKey(d => d.RecipeStep食譜Fk)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_STEP_步驟列表_RECIPE_食譜");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
