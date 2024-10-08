using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PrestaconnectWebService.Model.Prestaconnect.Class
{
    public partial class PrestaconnectDB : DbContext
    {
        public PrestaconnectDB()
            : base("name=PrestaconnectWebService.Properties.Settings.PrestaconnectConnection")
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleAdditionalField> ArticleAdditionalField { get; set; }
        public virtual DbSet<ArticleArchive> ArticleArchive { get; set; }
        public virtual DbSet<ArticleCustomField> ArticleCustomField { get; set; }
        public virtual DbSet<ArticleImage> ArticleImage { get; set; }
        public virtual DbSet<ArticleLang> ArticleLang { get; set; }
        public virtual DbSet<Attachment> Attachment { get; set; }
        public virtual DbSet<Attribute> Attribute { get; set; }
        public virtual DbSet<AttributeArticle> AttributeArticle { get; set; }
        public virtual DbSet<AttributeGroup> AttributeGroup { get; set; }
        public virtual DbSet<Carrier> Carrier { get; set; }
        public virtual DbSet<Catalog> Catalog { get; set; }
        public virtual DbSet<CatalogImage> CatalogImage { get; set; }
        public virtual DbSet<Characteristic> Characteristic { get; set; }
        public virtual DbSet<CompositionArticle> CompositionArticle { get; set; }
        public virtual DbSet<CompositionArticleAttribute> CompositionArticleAttribute { get; set; }
        public virtual DbSet<CompositionArticleAttributeGroup> CompositionArticleAttributeGroup { get; set; }
        public virtual DbSet<Conditioning> Conditioning { get; set; }
        public virtual DbSet<ConditioningArticle> ConditioningArticle { get; set; }
        public virtual DbSet<ConditioningGroup> ConditioningGroup { get; set; }
        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Employee_Collaborateur> Employee_Collaborateur { get; set; }
        public virtual DbSet<FMMRegistrationFields> FMMRegistrationFields { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Group_CRisque> Group_CRisque { get; set; }
        public virtual DbSet<ImportSageFilter> ImportSageFilter { get; set; }
        public virtual DbSet<InformationArticle> InformationArticle { get; set; }
        public virtual DbSet<InformationLibre> InformationLibre { get; set; }
        public virtual DbSet<InformationLibreArticle> InformationLibreArticle { get; set; }
        public virtual DbSet<InformationLibreClient> InformationLibreClient { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<LogInformations> LogInformations { get; set; }
        public virtual DbSet<MediaAssignmentRule> MediaAssignmentRule { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderCartRule> OrderCartRule { get; set; }
        public virtual DbSet<OrderDepotBy> OrderDepotBy { get; set; }
        public virtual DbSet<OrderMacketplace> OrderMacketplace { get; set; }
        public virtual DbSet<OrderMail> OrderMail { get; set; }
        public virtual DbSet<OrderMailRequest> OrderMailRequest { get; set; }
        public virtual DbSet<OrderPayment> OrderPayment { get; set; }
        public virtual DbSet<OrderPaymentOld> OrderPaymentOld { get; set; }
        public virtual DbSet<OrderSoucheBy> OrderSoucheBy { get; set; }
        public virtual DbSet<Replacement> Replacement { get; set; }
        public virtual DbSet<Settlement> Settlement { get; set; }
        public virtual DbSet<StatistiqueArticle> StatistiqueArticle { get; set; }
        public virtual DbSet<StatistiqueClient> StatistiqueClient { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<Supply> Supply { get; set; }
        public virtual DbSet<Tax> Tax { get; set; }
        public virtual DbSet<ArticleModifSpSage> ArticleModifSpSage { get; set; }
        public virtual DbSet<Lang> Lang { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .Property(e => e.Art_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Art_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Art_Description_Short)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Art_LinkRewrite)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Art_MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Art_MetaKeyword)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Art_MetaDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Art_Ref)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Art_Ean13)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Art_RedirectType)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Attachment)
                .WithRequired(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Characteristic)
                .WithRequired(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.ConditioningArticle)
                .WithRequired(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.ArticleAdditionalField)
                .WithRequired(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.CompositionArticle)
                .WithRequired(e => e.Article)
                .HasForeignKey(e => e.ComArt_ArtId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.CompositionArticleAttributeGroup)
                .WithRequired(e => e.Article)
                .HasForeignKey(e => e.Cag_ArtId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Catalog1)
                .WithMany(e => e.Article1)
                .Map(m => m.ToTable("ArticleCatalog").MapLeftKey("Art_Id").MapRightKey("Cat_Id"));

            modelBuilder.Entity<ArticleAdditionalField>()
                .Property(e => e.FieldName)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleAdditionalField>()
                .Property(e => e.FieldValue)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleAdditionalField>()
                .Property(e => e.FieldValue2)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleArchive>()
                .Property(e => e.Art_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleArchive>()
                .Property(e => e.Art_Description)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleArchive>()
                .Property(e => e.Art_Description_Short)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleArchive>()
                .Property(e => e.Art_LinkRewrite)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleArchive>()
                .Property(e => e.Art_MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleArchive>()
                .Property(e => e.Art_MetaKeyword)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleArchive>()
                .Property(e => e.Art_MetaDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleArchive>()
                .Property(e => e.Art_Ref)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleArchive>()
                .Property(e => e.Art_Ean13)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleArchive>()
                .Property(e => e.Art_RedirectType)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleCustomField>()
                .Property(e => e.Arc_ColonnePrestashop)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleCustomField>()
                .Property(e => e.Arc_TablePrestashop)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleCustomField>()
                .Property(e => e.Arc_ChampSage)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleCustomField>()
                .Property(e => e.Arc_ValeurSi)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleImage>()
                .Property(e => e.ImaArt_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleImage>()
                .Property(e => e.ImaArt_Image)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleImage>()
                .Property(e => e.ImaArt_SourceFile)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleImage>()
                .HasMany(e => e.AttributeArticle)
                .WithMany(e => e.ArticleImage)
                .Map(m => m.ToTable("AttributeArticleImage").MapLeftKey("ImaArt_Id").MapRightKey("AttArt_Id"));

            modelBuilder.Entity<ArticleImage>()
                .HasMany(e => e.CompositionArticle)
                .WithMany(e => e.ArticleImage)
                .Map(m => m.ToTable("CompositionArticleImage").MapLeftKey("ImaArt_Id").MapRightKey("ComArt_Id"));

            modelBuilder.Entity<ArticleLang>()
                .Property(e => e.Atl_Name)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleLang>()
                .Property(e => e.Atl_Description)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleLang>()
                .Property(e => e.Atl_Description_Short)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleLang>()
                .Property(e => e.Atl_MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleLang>()
                .Property(e => e.Atl_MetaKeyword)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleLang>()
                .Property(e => e.Atl_MetaDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.Att_File)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.Att_FileName)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.Att_Mime)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.Att_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Attachment>()
                .Property(e => e.Att_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Carrier>()
                .Property(e => e.Sag_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Carrier>()
                .Property(e => e.Pre_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog>()
                .Property(e => e.Cat_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog>()
                .Property(e => e.Cat_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog>()
                .Property(e => e.Cat_MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog>()
                .Property(e => e.Cat_MetaDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog>()
                .Property(e => e.Cat_MetaKeyword)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog>()
                .Property(e => e.Cat_LinkRewrite)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog>()
                .HasMany(e => e.Article)
                .WithRequired(e => e.Catalog)
                .HasForeignKey(e => e.Cat_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalog>()
                .HasMany(e => e.CatalogImage)
                .WithRequired(e => e.Catalog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CatalogImage>()
                .Property(e => e.ImaCat_Image)
                .IsUnicode(false);

            modelBuilder.Entity<Characteristic>()
                .Property(e => e.Cha_Value)
                .IsUnicode(false);

            modelBuilder.Entity<CompositionArticle>()
                .Property(e => e.ComArt_Quantity)
                .HasPrecision(20, 6);

            modelBuilder.Entity<CompositionArticle>()
                .HasMany(e => e.CompositionArticleAttribute)
                .WithRequired(e => e.CompositionArticle)
                .HasForeignKey(e => e.Caa_ComArtId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Config>()
                .Property(e => e.Con_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Config>()
                .Property(e => e.Con_Value)
                .IsUnicode(false);

            modelBuilder.Entity<FMMRegistrationFields>()
                .Property(e => e.Pre_FieldName)
                .IsUnicode(false);

            modelBuilder.Entity<FMMRegistrationFields>()
                .Property(e => e.Fmm_F_COMPTET)
                .IsUnicode(false);

            modelBuilder.Entity<ImportSageFilter>()
                .Property(e => e.Imp_Value)
                .IsUnicode(false);

            modelBuilder.Entity<ImportSageFilter>()
                .Property(e => e.Sag_Infolibre)
                .IsUnicode(false);

            modelBuilder.Entity<InformationLibre>()
                .Property(e => e.Sag_InfoLibre)
                .IsUnicode(false);

            modelBuilder.Entity<InformationLibreArticle>()
                .Property(e => e.Sag_InfoLibreArticle)
                .IsUnicode(false);

            modelBuilder.Entity<InformationLibreArticle>()
                .Property(e => e.Inf_Parent)
                .IsUnicode(false);

            modelBuilder.Entity<InformationLibreClient>()
                .Property(e => e.Sag_InfoLibreClient)
                .IsUnicode(false);

            modelBuilder.Entity<LogInformations>()
                .Property(e => e.NameLog)
                .IsUnicode(false);

            modelBuilder.Entity<LogInformations>()
                .Property(e => e.DescriptionLog)
                .IsUnicode(false);

            modelBuilder.Entity<LogInformations>()
                .Property(e => e.SolutionLog)
                .IsUnicode(false);

            modelBuilder.Entity<MediaAssignmentRule>()
                .Property(e => e.SuffixText)
                .IsUnicode(false);

            modelBuilder.Entity<MediaAssignmentRule>()
                .Property(e => e.AssignName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDepotBy>()
                .Property(e => e.Ord_Choise)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMacketplace>()
                .Property(e => e.Ord_ColoumName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMacketplace>()
                .Property(e => e.Ord_ReplaceText)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMacketplace>()
                .Property(e => e.Ord_MySQLRequest)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMail>()
                .Property(e => e.OrdMai_Header)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMail>()
                .Property(e => e.OrdMai_Content)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMail>()
                .Property(e => e.OrdMai_Expediteur)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMail>()
                .Property(e => e.OrdMai_CC)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMail>()
                .Property(e => e.OrdMai_ExpPwd)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMailRequest>()
                .Property(e => e.Mail_TagName)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMailRequest>()
                .Property(e => e.Mail_MySQLRequest)
                .IsUnicode(false);

            modelBuilder.Entity<OrderMailRequest>()
                .Property(e => e.Mail_IdOrderReplace)
                .IsUnicode(false);

            modelBuilder.Entity<OrderSoucheBy>()
                .Property(e => e.Ord_Choise)
                .IsUnicode(false);

            modelBuilder.Entity<Replacement>()
                .Property(e => e.OriginText)
                .IsUnicode(false);

            modelBuilder.Entity<Replacement>()
                .Property(e => e.ReplacementText)
                .IsUnicode(false);

            modelBuilder.Entity<Settlement>()
                .Property(e => e.Set_PaymentMethod)
                .IsUnicode(false);

            modelBuilder.Entity<Settlement>()
                .Property(e => e.Set_Journal)
                .IsUnicode(false);

            modelBuilder.Entity<Settlement>()
                .Property(e => e.Set_Intitule)
                .IsUnicode(false);

            modelBuilder.Entity<Settlement>()
                .Property(e => e.Com_SageRefArticle)
                .IsUnicode(false);

            modelBuilder.Entity<StatistiqueArticle>()
                .Property(e => e.Sag_StatArt)
                .IsUnicode(false);

            modelBuilder.Entity<StatistiqueClient>()
                .Property(e => e.Sag_StatClient)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Sup_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Sup_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Sup_MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Sup_MetaKeyword)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Sup_MetaDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Supply>()
                .Property(e => e.Sup_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Tax>()
                .Property(e => e.Sag_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Tax>()
                .Property(e => e.Pre_Name)
                .IsUnicode(false);

            modelBuilder.Entity<Tax>()
                .Property(e => e.Sag_ArticleRemise)
                .IsUnicode(false);

            modelBuilder.Entity<Tax>()
                .Property(e => e.Sag_ArticleRemplacement)
                .IsUnicode(false);

            modelBuilder.Entity<Tax>()
                .Property(e => e.Sag_ArticleReduction)
                .IsUnicode(false);

            modelBuilder.Entity<ArticleModifSpSage>()
                .Property(e => e.AR_REF)
                .IsFixedLength();

            modelBuilder.Entity<ArticleModifSpSage>()
                .Property(e => e.FA_CODEFAMILLE)
                .IsFixedLength();

            modelBuilder.Entity<ArticleModifSpSage>()
                .Property(e => e.CT_NUM)
                .IsFixedLength();
        }
    }
}
