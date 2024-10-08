using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Projetos_App1.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    
    public virtual DbSet<AttachedFile> AttachedFiles { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CompaniesCategory> CompaniesCategories { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyRelation> CompanyRelations { get; set; }

    public virtual DbSet<Complaint> Complaints { get; set; }

    public virtual DbSet<ComplaintStatus> ComplaintStatuses { get; set; }

    public virtual DbSet<Responsible> Responsibles { get; set; }

    public virtual DbSet<ResposibleHistory> ResposibleHistories { get; set; }

    public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }

    public virtual DbSet<Whistleblowing> Whistleblowings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=WENDEL-PC\\SQLEXPRESS;Initial Catalog=Complaint_Database;User Id=geova;Integrated Security=True;Encrypt=False");
    //Data Source=PC-INTERN004\\SQLEXPRESS;Initial Catalog=Complaint_Database;User Id=DESCONTEL\\jmatos;Integrated Security=True;Encrypt=False"
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttachedFile>(entity =>
        {
            entity.HasKey(e => e.AttachedFilesId).HasName("PK__Attached__C11F1DC7FB7F1EB7");

            entity.ToTable("Attached_Files");

            entity.Property(e => e.AttachedFilesId).HasColumnName("Attached_FilesID");
            entity.Property(e => e.ComplaintId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ComplaintID");
            entity.Property(e => e.FileType)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("File_type");
            entity.Property(e => e.FilesName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Image).HasColumnName("Image_");
            entity.Property(e => e.ImgSize).HasColumnName("Img_size");
            entity.Property(e => e.SubmissionDate)
                .HasColumnType("datetime")
                .HasColumnName("Submission_date");

            entity.HasOne(d => d.Complaint).WithMany(p => p.AttachedFiles)
                .HasForeignKey(d => d.ComplaintId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ComplaintID");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B29DD2205");

            entity.ToTable("Category");

            entity.HasIndex(e => e.Categories, "UQ__Category__05299DB916CB88DD").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Categories)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CompaniesCategory>(entity =>
        {
            entity.HasKey(e => e.CompaniesCategoryId).HasName("PK__Company___F4F5C4E3CFDD3FF6");

            entity.ToTable("Companies_Category");

            entity.Property(e => e.CompaniesCategoryId).HasColumnName("Companies_CategoryID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CompaniesId).HasColumnName("CompaniesID");

            entity.HasOne(d => d.Category).WithMany(p => p.CompaniesCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Company_C__Categ__5FB337D6");

            entity.HasOne(d => d.Companies).WithMany(p => p.CompaniesCategories)
                .HasForeignKey(d => d.CompaniesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Company_C__Compa__5EBF139D");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompaniesId).HasName("PK__Companie__5C0F2CAEB5B1F02D");

            entity.HasIndex(e => e.Niss, "UQ_Companies_NISS").IsUnique();

            entity.Property(e => e.CompaniesId).HasColumnName("CompaniesID");
            entity.Property(e => e.Adreess)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Niss)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("NISS");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CompanyRelation>(entity =>
        {
            entity.HasKey(e => e.CompanyRelationId).HasName("PK__Company___FC4430E96FE8AE32");

            entity.ToTable("Company_Relation");

            entity.HasIndex(e => e.CompanyRelationship, "UQ__Company___58CCF2F64BAE9CA7").IsUnique();

            entity.Property(e => e.CompanyRelationId).HasColumnName("Company_RelationID");
            entity.Property(e => e.CompanyRelationship)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Company_Relationship");
        });

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(e => e.ComplaintId).HasName("PK__Complain__740D89AF2BD6BC14");

            entity.ToTable("Complaint");

            entity.Property(e => e.ComplaintId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ComplaintID");
            entity.Property(e => e.CompaniesCategoryId).HasColumnName("Companies_CategoryID");
            entity.Property(e => e.CompanyRelationId).HasColumnName("Company_RelationID");
            entity.Property(e => e.ComplaintCloseDate)
                .HasColumnType("datetime")
                .HasColumnName("Complaint_close_date");
            entity.Property(e => e.ComplaintDescription)
                .HasColumnType("text")
                .HasColumnName("Complaint_Description");
            entity.Property(e => e.ComplaintResponse)
                .HasColumnType("text")
                .HasColumnName("Complaint_response");
            entity.Property(e => e.ComplaintStartDate)
                .HasColumnType("datetime")
                .HasColumnName("Complaint_start_date");
            entity.Property(e => e.ComplaintStatusId).HasColumnName("Complaint_StatusID");
            entity.Property(e => e.ComplaintSubject)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Complaint_Subject");
            entity.Property(e => e.ComplaintType).HasColumnName("Complaint_Type");
            entity.Property(e => e.CurrentResponsibleId).HasColumnName("Current_ResponsibleID");
            entity.Property(e => e.PassWord)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Pass_Word");
            entity.Property(e => e.ShippingMethodsId).HasColumnName("Shipping_MethodsID");

            entity.HasOne(d => d.CompaniesCategory).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.CompaniesCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Complaint__Compa__6D0D32F4");

            entity.HasOne(d => d.CompanyRelation).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.CompanyRelationId)
                .HasConstraintName("FK__Complaint__Compa__6FE99F9F");

            entity.HasOne(d => d.ComplaintStatus).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.ComplaintStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Complaint__Compl__6EF57B66");

            entity.HasOne(d => d.CurrentResponsible).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.CurrentResponsibleId)
                .HasConstraintName("Current_ResponsibleID");

            entity.HasOne(d => d.ShippingMethods).WithMany(p => p.Complaints)
                .HasForeignKey(d => d.ShippingMethodsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Complaint__Shipp__6E01572D");
        });

        modelBuilder.Entity<ComplaintStatus>(entity =>
        {
            entity.HasKey(e => e.ComplaintStatusId).HasName("PK__Coplaint__2937804F13139A0F");

            entity.ToTable("Complaint_Status");

            entity.HasIndex(e => e.CurrentStatus, "UQ__Coplaint__ED1950C5DDC126BF").IsUnique();

            entity.Property(e => e.ComplaintStatusId).HasColumnName("Complaint_StatusID");
            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Current_Status");
        });

        modelBuilder.Entity<Responsible>(entity =>
        {
            entity.HasKey(e => e.ResponsiblesId).HasName("PK__Responsi__5012A63720065FCD");

            entity.Property(e => e.ResponsiblesId).HasColumnName("ResponsiblesID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.ResponsibleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Responsible_Name");
        });

        modelBuilder.Entity<ResposibleHistory>(entity =>
        {
            entity.HasKey(e => e.ResposibleHistory1).HasName("PK__Resposib__F0F4465873B7CDF3");

            entity.ToTable("Resposible_History");

            entity.Property(e => e.ResposibleHistory1).HasColumnName("Resposible_History");
            entity.Property(e => e.ComplaintId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ComplaintID");
            entity.Property(e => e.DataIn)
                .HasColumnType("datetime")
                .HasColumnName("Data_in");
            entity.Property(e => e.DataOu)
                .HasColumnType("datetime")
                .HasColumnName("Data_ou");
            entity.Property(e => e.ResponsiblesId).HasColumnName("ResponsiblesID");

            entity.HasOne(d => d.Complaint).WithMany(p => p.ResposibleHistories)
                .HasForeignKey(d => d.ComplaintId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Complaint");

            entity.HasOne(d => d.Responsibles).WithMany(p => p.ResposibleHistories)
                .HasForeignKey(d => d.ResponsiblesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Resposibl__Respo__787EE5A0");
        });

        modelBuilder.Entity<ShippingMethod>(entity =>
        {
            entity.HasKey(e => e.ShippingMethodsId).HasName("PK__Shipping__97F05626833A0B45");

            entity.ToTable("Shipping_Methods");

            entity.HasIndex(e => e.Type, "UQ__Shipping__F9B8A48B144DF377").IsUnique();

            entity.Property(e => e.ShippingMethodsId).HasColumnName("Shipping_MethodsID");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Whistleblowing>(entity =>
        {
            entity.HasKey(e => e.WhistleblowingId).HasName("PK__Whistleb__285FAD1AAD6AE8E5");

            entity.ToTable("Whistleblowing");

            entity.HasIndex(e => e.ComplaintId, "UQ__Whistleb__740D89AED733990C").IsUnique();

            entity.Property(e => e.WhistleblowingId).HasColumnName("WhistleblowingID");
            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ComplaintId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ComplaintID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Name_");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.Complaint).WithOne(p => p.Whistleblowing)
                .HasForeignKey<Whistleblowing>(d => d.ComplaintId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Whistlebl__Compl__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
