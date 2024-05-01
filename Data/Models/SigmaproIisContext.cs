using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class SigmaproIisContext : DbContext
{
    public SigmaproIisContext()
    {
    }

    public SigmaproIisContext(DbContextOptions<SigmaproIisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<BusinessConfiguration> BusinessConfigurations { get; set; }

    public virtual DbSet<CitiesMasterDoNotUse> CitiesMasterDoNotUses { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<CountiesMaster> CountiesMasters { get; set; }

    public virtual DbSet<CountriesMaster> CountriesMasters { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<County> Counties { get; set; }

    public virtual DbSet<Cpt> Cpts { get; set; }

    public virtual DbSet<CptCvxMasterDoNotUse> CptCvxMasterDoNotUses { get; set; }

    public virtual DbSet<Cvx> Cvxes { get; set; }

    public virtual DbSet<CvxMasterDoNotUse> CvxMasterDoNotUses { get; set; }

    public virtual DbSet<CvxMvxProductMappingDoNotUse> CvxMvxProductMappingDoNotUses { get; set; }

    public virtual DbSet<CvxVaccineGroup> CvxVaccineGroups { get; set; }

    public virtual DbSet<CvxVaccineGroupMasterDoNotUse> CvxVaccineGroupMasterDoNotUses { get; set; }

    public virtual DbSet<CvxVi> CvxVis { get; set; }

    public virtual DbSet<CvxVisMasterDoNotUse> CvxVisMasterDoNotUses { get; set; }

    public virtual DbSet<EntityAddress> EntityAddresses { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<Filter> Filters { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Juridiction> Juridictions { get; set; }

    public virtual DbSet<LovMaster> LovMasters { get; set; }

    public virtual DbSet<Mvx> Mvxes { get; set; }

    public virtual DbSet<MvxMasterDoNotUse> MvxMasterDoNotUses { get; set; }

    public virtual DbSet<Ndc> Ndcs { get; set; }

    public virtual DbSet<NdclookupDoNotUse> NdclookupDoNotUses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientDuplicateRecord> PatientDuplicateRecords { get; set; }

    public virtual DbSet<PatientNewRecord> PatientNewRecords { get; set; }

    public virtual DbSet<PatientStage> PatientStages { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<Site> Sites { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<StatesMaster> StatesMasters { get; set; }

    public virtual DbSet<TermsCondition> TermsConditions { get; set; }

    public virtual DbSet<TestAble> TestAbles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VaccinePrice> VaccinePrices { get; set; }

    public virtual DbSet<VaccinePriceDoNotUse> VaccinePriceDoNotUses { get; set; }

    public virtual DbSet<VaccinepriceBkp> VaccinepriceBkps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=sigmaprodb.postgres.database.azure.com,5432;Database=sigmapro_iis;Username=sigmaprodb_user;Password=Rules@23$$11;TrustServerCertificate=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Addresses_pkey");

            entity.ToTable("addresses");

            entity.HasIndex(e => e.CountyId, "fki_fk_countyids");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AddressId)
                .HasColumnType("character varying")
                .HasColumnName("address_id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountyId).HasColumnName("county_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Line1)
                .HasColumnType("character varying")
                .HasColumnName("line1");
            entity.Property(e => e.Line2)
                .HasColumnType("character varying")
                .HasColumnName("line2");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.Suite)
                .HasColumnType("character varying")
                .HasColumnName("suite");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.ZipCode)
                .HasColumnType("character varying")
                .HasColumnName("zip_code");

            entity.HasOne(d => d.City).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("fk_cityids");

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("fk_countryids");

            entity.HasOne(d => d.County).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountyId)
                .HasConstraintName("fk_countyids");

            entity.HasOne(d => d.State).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("fk_stateids");
        });

        modelBuilder.Entity<BusinessConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Business_Configuration_pkey");

            entity.ToTable("business_configuration");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AddressIdStart)
                .HasColumnType("character varying")
                .HasColumnName("address_id_start");
            entity.Property(e => e.AddressIdSuffix)
                .HasColumnType("character varying")
                .HasColumnName("address_id_suffix");
            entity.Property(e => e.BusinessId)
                .HasColumnType("character varying")
                .HasColumnName("Business_id");
            entity.Property(e => e.BusinessName)
                .HasColumnType("character varying")
                .HasColumnName("Business name");
            entity.Property(e => e.EmailId)
                .HasColumnType("character varying")
                .HasColumnName("email id");
            entity.Property(e => e.FaciltyIdStart)
                .HasColumnType("character varying")
                .HasColumnName("facilty_id_start");
            entity.Property(e => e.FaciltyIdSuffix)
                .HasColumnType("character varying")
                .HasColumnName("facilty_id_suffix");
            entity.Property(e => e.JurisdictionIdStart)
                .HasColumnType("character varying")
                .HasColumnName("Jurisdiction_id_start");
            entity.Property(e => e.JusrisidictionIdSuffix)
                .HasColumnType("character varying")
                .HasColumnName("Jusrisidiction_id_suffix");
            entity.Property(e => e.OrganizationIdStart)
                .HasColumnType("character varying")
                .HasColumnName("organization_id_start");
            entity.Property(e => e.OrganizationIdSuffix)
                .HasColumnType("character varying")
                .HasColumnName("organization_id_suffix");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.ProviderIdStart)
                .HasColumnType("character varying")
                .HasColumnName("provider_id_start");
            entity.Property(e => e.ProviderIdSuffix)
                .HasColumnType("character varying")
                .HasColumnName("provider_id_suffix");
            entity.Property(e => e.SiteIdStart)
                .HasColumnType("character varying")
                .HasColumnName("site_id_start");
            entity.Property(e => e.SiteIdSuffix)
                .HasColumnType("character varying")
                .HasColumnName("site_id_suffix");
        });

        modelBuilder.Entity<CitiesMasterDoNotUse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cities_master_do_not_use");

            entity.Property(e => e.CityName)
                .HasColumnType("character varying")
                .HasColumnName("city_name");
            entity.Property(e => e.County)
                .HasColumnType("character varying")
                .HasColumnName("county");
            entity.Property(e => e.State)
                .HasColumnType("character varying")
                .HasColumnName("state");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cities_pkey");

            entity.ToTable("cities");

            entity.HasIndex(e => e.CountyId, "fki_fk_countyid");

            entity.HasIndex(e => e.StateId, "fki_fk_stateid");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CityId)
                .ValueGeneratedOnAdd()
                .HasColumnName("city_id");
            entity.Property(e => e.CityName)
                .HasColumnType("character varying")
                .HasColumnName("city_name");
            entity.Property(e => e.CountyId).HasColumnName("county_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.County).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountyId)
                .HasConstraintName("fk_countyid");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("fk_stateid");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Contacts_pkey");

            entity.ToTable("contacts");

            entity.HasIndex(e => e.EntityId, "fki_entity_organization_contact");

            entity.HasIndex(e => e.EntityId, "fki_entity_provider_contact");

            entity.HasIndex(e => e.EntityId, "fki_entity_site_contact");

            entity.HasIndex(e => e.EntityId, "fki_f");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ContactType)
                .HasColumnType("character varying")
                .HasColumnName("contact_type");
            entity.Property(e => e.ContactValue)
                .HasColumnType("character varying")
                .HasColumnName("contact_value");
            entity.Property(e => e.ContactsId)
                .HasColumnType("character varying")
                .HasColumnName("contacts_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityType)
                .HasColumnType("character varying")
                .HasColumnName("entity_type");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.Isprimary)
                .HasDefaultValue(false)
                .HasColumnName("isprimary");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<CountiesMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("counties_master");

            entity.Property(e => e.CountyCode)
                .HasColumnType("character varying")
                .HasColumnName("county_code");
            entity.Property(e => e.CountyName)
                .HasColumnType("character varying")
                .HasColumnName("county_name");
            entity.Property(e => e.State)
                .HasColumnType("character varying")
                .HasColumnName("state");
        });

        modelBuilder.Entity<CountriesMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("countries_master");

            entity.Property(e => e.Countryname).HasColumnName("countryname");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Countries_pkey");

            entity.ToTable("countries");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Alpha2code)
                .HasColumnType("character varying")
                .HasColumnName("alpha2code");
            entity.Property(e => e.Alpha3code)
                .HasColumnType("character varying")
                .HasColumnName("alpha3code");
            entity.Property(e => e.CountryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasColumnType("character varying")
                .HasColumnName("country_name");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<County>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Counties_pkey");

            entity.ToTable("counties");

            entity.HasIndex(e => e.StateId, "fki_fk_stateids");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CountyCode)
                .HasColumnType("character varying")
                .HasColumnName("county_code");
            entity.Property(e => e.CountyId)
                .ValueGeneratedOnAdd()
                .HasColumnName("county_id");
            entity.Property(e => e.CountyName)
                .HasColumnType("character varying")
                .HasColumnName("county_name");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.State).WithMany(p => p.Counties)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("fk_stateids");
        });

        modelBuilder.Entity<Cpt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CPT_pkey");

            entity.ToTable("cpt");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasColumnType("character varying")
                .HasColumnName("comment");
            entity.Property(e => e.CptCode)
                .HasColumnType("character varying")
                .HasColumnName("cpt_code");
            entity.Property(e => e.CptCodeId)
                .HasColumnType("character varying")
                .HasColumnName("cpt_code_id");
            entity.Property(e => e.CptDescription)
                .HasColumnType("character varying")
                .HasColumnName("cpt_description");
            entity.Property(e => e.CptId)
                .ValueGeneratedOnAdd()
                .HasColumnName("cpt_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CvxCodeId).HasColumnName("cvx_code_id");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<CptCvxMasterDoNotUse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CPT_CVX_master_do_not_use");

            entity.Property(e => e.Comment)
                .HasColumnType("character varying")
                .HasColumnName("comment");
            entity.Property(e => e.CptCode)
                .HasColumnType("character varying")
                .HasColumnName("CPT_code");
            entity.Property(e => e.CptCodeId)
                .HasColumnType("character varying")
                .HasColumnName("CPT_Code_ID");
            entity.Property(e => e.CptDescription)
                .HasColumnType("character varying")
                .HasColumnName("CPT_description");
            entity.Property(e => e.CvxCode)
                .HasColumnType("character varying")
                .HasColumnName("cvx_code");
            entity.Property(e => e.CvxShortDescription)
                .HasColumnType("character varying")
                .HasColumnName("CVX_short_description");
            entity.Property(e => e.LastUpdated).HasColumnName("last_updated");
        });

        modelBuilder.Entity<Cvx>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CVX_pkey");

            entity.ToTable("cvx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CvxCode)
                .HasColumnType("character varying")
                .HasColumnName("cvx_code");
            entity.Property(e => e.CvxDescription)
                .HasColumnType("character varying")
                .HasColumnName("cvx_description");
            entity.Property(e => e.CvxId)
                .ValueGeneratedOnAdd()
                .HasColumnName("cvx_id");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.NonVaccine)
                .HasColumnType("character varying")
                .HasColumnName("non_vaccine");
            entity.Property(e => e.Note)
                .HasColumnType("character varying")
                .HasColumnName("note");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.VaccineName)
                .HasColumnType("character varying")
                .HasColumnName("vaccine_name");
            entity.Property(e => e.VaccineStatus)
                .HasColumnType("character varying")
                .HasColumnName("vaccine_status");
        });

        modelBuilder.Entity<CvxMasterDoNotUse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CVX_master_do_not_use");

            entity.Property(e => e.CvxCode).HasColumnName("CVX_code");
            entity.Property(e => e.CvxDescription)
                .HasColumnType("character varying")
                .HasColumnName("CVX_description");
            entity.Property(e => e.NonVaccine)
                .HasColumnType("character varying")
                .HasColumnName("non_vaccine");
            entity.Property(e => e.Note)
                .HasColumnType("character varying")
                .HasColumnName("note");
            entity.Property(e => e.UpdateDate).HasColumnName("update_date");
            entity.Property(e => e.VaccineName)
                .HasColumnType("character varying")
                .HasColumnName("vaccine_name");
            entity.Property(e => e.VaccineStatus)
                .HasColumnType("character varying")
                .HasColumnName("vaccine_status");
        });

        modelBuilder.Entity<CvxMvxProductMappingDoNotUse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CVX_MVX_product_mapping_do_not_use");

            entity.Property(e => e.CvxCode)
                .HasColumnType("character varying")
                .HasColumnName("CVX Code");
            entity.Property(e => e.CvxShortDescription)
                .HasColumnType("character varying")
                .HasColumnName("CVX Short Description");
            entity.Property(e => e.ManufacturerTblManufacturerName)
                .HasColumnType("character varying")
                .HasColumnName("manufacturer_TBL.manufacturer_name");
            entity.Property(e => e.ManufacturerTblMvxCode)
                .HasColumnType("character varying")
                .HasColumnName("manufacturer_TBL.MVX_CODE");
            entity.Property(e => e.MvxStatus)
                .HasColumnType("character varying")
                .HasColumnName("MVX status");
            entity.Property(e => e.ProductNameStatus)
                .HasColumnType("character varying")
                .HasColumnName("product name status");
            entity.Property(e => e.ProductTblProductName)
                .HasColumnType("character varying")
                .HasColumnName("product_TBL.productName");
            entity.Property(e => e.ProductTblUpdateDate).HasColumnName("product_TBL.Update_date");
        });

        modelBuilder.Entity<CvxVaccineGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CVX_Vaccine_Group_pkey");

            entity.ToTable("cvx_vaccine_group");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CvxCodeId).HasColumnName("cvx_code_id");
            entity.Property(e => e.CvxForVaccineGroup)
                .HasColumnType("character varying")
                .HasColumnName("cvx_for_vaccine_group");
            entity.Property(e => e.CvxVaccineId)
                .ValueGeneratedOnAdd()
                .HasColumnName("cvx_vaccine_id");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.VaccineGroupName)
                .HasColumnType("character varying")
                .HasColumnName("vaccine_group_name");
            entity.Property(e => e.VaccineStatus)
                .HasColumnType("character varying")
                .HasColumnName("vaccine_status");
        });

        modelBuilder.Entity<CvxVaccineGroupMasterDoNotUse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CVX_VaccineGroup_master_do_not_use");

            entity.Property(e => e.CvxCode)
                .HasColumnType("character varying")
                .HasColumnName("CVX _Code");
            entity.Property(e => e.CvxDescription)
                .HasColumnType("character varying")
                .HasColumnName("CVX _description");
            entity.Property(e => e.CvxForVaccineGroup)
                .HasColumnType("character varying")
                .HasColumnName(" CVX  _ for_VaccineGroup");
            entity.Property(e => e.VaccineGroupName)
                .HasColumnType("character varying")
                .HasColumnName("VaccineGroup_ Name");
            entity.Property(e => e.VaccineStatus)
                .HasColumnType("character varying")
                .HasColumnName("Vaccine _Status");
        });

        modelBuilder.Entity<CvxVi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CVX_VIS_pkey");

            entity.ToTable("cvx_vis");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CvxCodeId).HasColumnName("cvx_code_id");
            entity.Property(e => e.CvxVisId)
                .ValueGeneratedOnAdd()
                .HasColumnName("cvx_vis_id");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.VisDocumentName)
                .HasColumnType("character varying")
                .HasColumnName("vis_document_name");
            entity.Property(e => e.VisEditionDate).HasColumnName("vis_edition_date");
            entity.Property(e => e.VisEditionStatus)
                .HasColumnType("character varying")
                .HasColumnName("vis_edition_status");
            entity.Property(e => e.VisFullyEncodedText)
                .HasColumnType("character varying")
                .HasColumnName("vis_fully_encoded_text");
        });

        modelBuilder.Entity<CvxVisMasterDoNotUse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CVX_VIS_master_do_not_use");

            entity.Property(e => e.CvxCode)
                .HasColumnType("character varying")
                .HasColumnName("CVX_code");
            entity.Property(e => e.CvxVaccineDescription)
                .HasColumnType("character varying")
                .HasColumnName("CVX _Vaccine_ Description");
            entity.Property(e => e.VisDocumentName)
                .HasColumnType("character varying")
                .HasColumnName("VIS _Document _Name");
            entity.Property(e => e.VisEditionDate).HasColumnName("VIS_Edition_Date");
            entity.Property(e => e.VisEditionStatus)
                .HasColumnType("character varying")
                .HasColumnName("VIS_Edition_Status");
            entity.Property(e => e.VisFullyEncodedTextString)
                .HasColumnType("character varying")
                .HasColumnName("VIS _Fully-encoded_ text _string");
        });

        modelBuilder.Entity<EntityAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("entity_address_pkey");

            entity.ToTable("entity_address");

            entity.HasIndex(e => e.EntityId, "fki_entity_facility_id");

            entity.HasIndex(e => e.EntityId, "fki_entity_organization_id");

            entity.HasIndex(e => e.EntityId, "fki_entity_provider_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AddressType)
                .HasColumnType("character varying")
                .HasColumnName("address_type");
            entity.Property(e => e.Addressid).HasColumnName("addressid");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityType)
                .HasColumnType("character varying")
                .HasColumnName("entity_type");
            entity.Property(e => e.Isprimary).HasColumnName("isprimary");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_pkey");

            entity.ToTable("event");

            entity.HasIndex(e => e.ProviderId, "fki_provider_fk");

            entity.HasIndex(e => e.ProviderId, "fki_provider_id");

            entity.HasIndex(e => e.SiteId, "fki_site_fk");

            entity.HasIndex(e => e.CvxCodeId, "fki_vaccine_fk");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CvxCodeId).HasColumnName("cvx_code_id");
            entity.Property(e => e.EventDate).HasColumnName("event_date");
            entity.Property(e => e.EventId)
                .ValueGeneratedOnAdd()
                .HasColumnName("event_id");
            entity.Property(e => e.EventName)
                .HasColumnType("character varying")
                .HasColumnName("event_name");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.SiteId).HasColumnName("site_id");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.CvxCode).WithMany(p => p.Events)
                .HasForeignKey(d => d.CvxCodeId)
                .HasConstraintName("vaccine_fk");

            entity.HasOne(d => d.Provider).WithMany(p => p.Events)
                .HasForeignKey(d => d.ProviderId)
                .HasConstraintName("provider_fk");

            entity.HasOne(d => d.Site).WithMany(p => p.Events)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("site_fk");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Facilities_pkey");

            entity.ToTable("facilities");

            entity.HasIndex(e => e.UserId, "fki_fk_facility_user_id");

            entity.HasIndex(e => e.OrganizationsId, "fki_organizations_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AdministeredAtLocation)
                .HasColumnType("character varying")
                .HasColumnName("administered_at_location");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.FacilityId)
                .HasColumnType("character varying")
                .HasColumnName("facility_id");
            entity.Property(e => e.FacilityName)
                .HasColumnType("character varying")
                .HasColumnName("facility_name");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.OrganizationsId).HasColumnName("organizations_id");
            entity.Property(e => e.ResponsibleOrganization)
                .HasColumnType("character varying")
                .HasColumnName("responsible_organization");
            entity.Property(e => e.SendingOrganization)
                .HasColumnType("character varying")
                .HasColumnName("sending_organization");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Organizations).WithMany(p => p.Facilities)
                .HasForeignKey(d => d.OrganizationsId)
                .HasConstraintName("organizations_id");

            entity.HasOne(d => d.User).WithMany(p => p.Facilities)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_facility_user_id");
        });

        modelBuilder.Entity<Filter>(entity =>
        {
            entity.HasKey(e => e.FilterId).HasName("filters_pkey");

            entity.ToTable("filters");

            entity.Property(e => e.FilterId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("filter_id");
            entity.Property(e => e.FilterCondition)
                .HasMaxLength(255)
                .HasColumnName("filter_condition");
            entity.Property(e => e.FilterType)
                .HasMaxLength(50)
                .HasColumnName("filter_type");
            entity.Property(e => e.LogicalOperator)
                .HasMaxLength(5)
                .HasColumnName("logical_operator");
            entity.Property(e => e.PageName)
                .HasMaxLength(50)
                .HasColumnName("page_name");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Inventory_pkey");

            entity.ToTable("inventory");

            entity.HasIndex(e => e.FacilityId, "fki_fk_inv_facility_id");

            entity.HasIndex(e => e.ProductId, "fki_fk_inv_prod_id");

            entity.HasIndex(e => e.SiteId, "fki_fk_inv_site_id");

            entity.HasIndex(e => e.UserId, "fki_fk_inv_user_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.ExpirationDate).HasColumnName("expiration_date");
            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.InventoryDate).HasColumnName("inventory_date");
            entity.Property(e => e.InventoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("inventory_id");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.QuantityRemaining)
                .HasColumnType("character varying")
                .HasColumnName("quantity_remaining");
            entity.Property(e => e.SiteId).HasColumnName("site_id");
            entity.Property(e => e.TempRecorded)
                .HasColumnType("character varying")
                .HasColumnName("temp_recorded");
            entity.Property(e => e.UnitOfTemp)
                .HasColumnType("character varying")
                .HasColumnName("unit_of_temp");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Facility).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("fk_inv_facility_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_inv_prod_id");

            entity.HasOne(d => d.Site).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.SiteId)
                .HasConstraintName("fk_inv_site_id");

            entity.HasOne(d => d.User).WithMany(p => p.Inventories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_inv_user_id");
        });

        modelBuilder.Entity<Juridiction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Juridictions_pkey");

            entity.ToTable("juridictions");

            entity.HasIndex(e => e.AlternateId, "fki_fk_alternate_id");

            entity.HasIndex(e => e.StateId, "fki_fk_stateid_jurd");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AlternateId).HasColumnName("alternate_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.JuridictionId)
                .HasColumnType("character varying")
                .HasColumnName("juridiction_id");
            entity.Property(e => e.JuridictionName)
                .HasColumnType("character varying")
                .HasColumnName("juridiction_name");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.Alternate).WithMany(p => p.Juridictions)
                .HasForeignKey(d => d.AlternateId)
                .HasConstraintName("fk_alternate_id");

            entity.HasOne(d => d.State).WithMany(p => p.Juridictions)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("fk_stateid_jurd");
        });

        modelBuilder.Entity<LovMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LOV_master_pkey");

            entity.ToTable("lov_master");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.Key)
                .HasColumnType("character varying")
                .HasColumnName("key");
            entity.Property(e => e.LongDescription)
                .HasColumnType("character varying")
                .HasColumnName("long_description");
            entity.Property(e => e.LovType)
                .HasColumnType("character varying")
                .HasColumnName("LOV_type");
            entity.Property(e => e.ReferenceId)
                .ValueGeneratedOnAdd()
                .HasColumnName("reference_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.Value)
                .HasColumnType("character varying")
                .HasColumnName("value");
        });

        modelBuilder.Entity<Mvx>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MVX_pkey");

            entity.ToTable("mvx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.ManufacturerName)
                .HasColumnType("character varying")
                .HasColumnName("manufacturer_name");
            entity.Property(e => e.MvxCode)
                .HasColumnType("character varying")
                .HasColumnName("mvx_code");
            entity.Property(e => e.MvxId)
                .ValueGeneratedOnAdd()
                .HasColumnName("mvx_id");
            entity.Property(e => e.Notes)
                .HasColumnType("character varying")
                .HasColumnName("notes");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<MvxMasterDoNotUse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MVX_master_do_not_use");

            entity.Property(e => e.Lastupdateddate).HasColumnName("lastupdateddate");
            entity.Property(e => e.ManufacturerId)
                .HasColumnType("character varying")
                .HasColumnName("manufacturer_id");
            entity.Property(e => e.ManufacturerName)
                .HasColumnType("character varying")
                .HasColumnName("manufacturer_name");
            entity.Property(e => e.MvxCode)
                .HasColumnType("character varying")
                .HasColumnName("MVX_CODE");
            entity.Property(e => e.Notes).HasColumnType("character varying");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Ndc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ndc_pkey");

            entity.ToTable("ndc");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CvxId).HasColumnName("cvx_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("character varying")
                .HasColumnName("end_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.MvxId).HasColumnName("mvx_id");
            entity.Property(e => e.NdcId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ndc_id");
            entity.Property(e => e.NouseNdc)
                .HasColumnType("character varying")
                .HasColumnName("nouse_ndc");
            entity.Property(e => e.SaleDate)
                .HasColumnType("character varying")
                .HasColumnName("sale_date");
            entity.Property(e => e.SaleGtin)
                .HasColumnType("character varying")
                .HasColumnName("sale_gtin");
            entity.Property(e => e.SaleLabler)
                .HasColumnType("character varying")
                .HasColumnName("sale_labler");
            entity.Property(e => e.SaleLastUpdate)
                .HasColumnType("character varying")
                .HasColumnName("sale_last_update");
            entity.Property(e => e.SaleNdc11)
                .HasColumnType("character varying")
                .HasColumnName("sale_ndc11");
            entity.Property(e => e.SaleProprietaryName)
                .HasColumnType("character varying")
                .HasColumnName("sale_proprietary_name");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.UseGtin)
                .HasColumnType("character varying")
                .HasColumnName("use_gtin");
            entity.Property(e => e.UseLastUpdate)
                .HasColumnType("character varying")
                .HasColumnName("use_last_update");
            entity.Property(e => e.UseNdc11)
                .HasColumnType("character varying")
                .HasColumnName("use_ndc11");
        });

        modelBuilder.Entity<NdclookupDoNotUse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("NDCLookup_do_not_use");

            entity.Property(e => e.CvxCode)
                .HasColumnType("character varying")
                .HasColumnName("CVX Code");
            entity.Property(e => e.CvxDescription)
                .HasColumnType("character varying")
                .HasColumnName("CVX Description");
            entity.Property(e => e.EndDate)
                .HasColumnType("character varying")
                .HasColumnName("End Date");
            entity.Property(e => e.MvxCode)
                .HasColumnType("character varying")
                .HasColumnName("MVX Code");
            entity.Property(e => e.NoUseNdc)
                .HasColumnType("character varying")
                .HasColumnName("No use NDC");
            entity.Property(e => e.SaleGtin)
                .HasColumnType("character varying")
                .HasColumnName("Sale GTIN");
            entity.Property(e => e.SaleLabeler)
                .HasColumnType("character varying")
                .HasColumnName("Sale Labeler");
            entity.Property(e => e.SaleLastUpdate)
                .HasColumnType("character varying")
                .HasColumnName("Sale Last Update");
            entity.Property(e => e.SaleNdc11)
                .HasColumnType("character varying")
                .HasColumnName("Sale NDC11");
            entity.Property(e => e.SaleProprietaryName)
                .HasColumnType("character varying")
                .HasColumnName("Sale Proprietary Name");
            entity.Property(e => e.StartDate)
                .HasColumnType("character varying")
                .HasColumnName("Start Date");
            entity.Property(e => e.UseGtin)
                .HasColumnType("character varying")
                .HasColumnName("Use GTIN");
            entity.Property(e => e.UseLastUpdate)
                .HasColumnType("character varying")
                .HasColumnName("Use Last Update");
            entity.Property(e => e.UseNdc11)
                .HasColumnType("character varying")
                .HasColumnName("Use NDC11");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Order_pkey");

            entity.ToTable("order");

            entity.HasIndex(e => e.FacilityId, "fki_fk_ord_facility_id");

            entity.HasIndex(e => e.UserId, "fki_fk_ord_userid");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DiscountAmount)
                .HasColumnType("character varying")
                .HasColumnName("discount_amount");
            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.Incoterms)
                .HasColumnType("character varying")
                .HasColumnName("incoterms");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("order_id");
            entity.Property(e => e.OrderStatus)
                .HasColumnType("character varying")
                .HasColumnName("order_status");
            entity.Property(e => e.OrderTotal)
                .HasColumnType("character varying")
                .HasColumnName("order_total");
            entity.Property(e => e.TaxAmount)
                .HasColumnType("character varying")
                .HasColumnName("tax_amount");
            entity.Property(e => e.TermsConditionsId).HasColumnName("terms_conditions_id");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Facility).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("fk_ord_facility_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_ord_userid");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OrderItem_pkey");

            entity.ToTable("order_item");

            entity.HasIndex(e => e.OrderId, "fki_fk_order_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.OrderItemDesc)
                .HasColumnType("character varying")
                .HasColumnName("order_item_desc");
            entity.Property(e => e.OrderItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("order_item_id");
            entity.Property(e => e.OrderItemStatus)
                .HasColumnType("character varying")
                .HasColumnName("order_item_status");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasColumnType("character varying")
                .HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("character varying")
                .HasColumnName("unit_price");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_order_id");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Organizations_pkey");

            entity.ToTable("organizations");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.JuridictionId).HasColumnName("juridiction_id");
            entity.Property(e => e.OrganizationName)
                .HasColumnType("character varying")
                .HasColumnName("organization_name");
            entity.Property(e => e.OrganizationsId)
                .HasColumnType("character varying")
                .HasColumnName("organizations_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.Address).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("fk_org_address_id");

            entity.HasOne(d => d.Juridiction).WithMany(p => p.Organizations)
                .HasForeignKey(d => d.JuridictionId)
                .HasConstraintName("fk_juridiction_id");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Patient_pkey");

            entity.ToTable("patient");

            entity.HasIndex(e => e.PersonId, "fki_fk_patient_person_id");

            entity.HasIndex(e => e.CityId, "fki_patient_city_id");

            entity.HasIndex(e => e.CountryId, "fki_patient_country_id");

            entity.HasIndex(e => e.StateId, "fki_s");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AliasFirstName)
                .HasColumnType("character varying")
                .HasColumnName("alias_first_name");
            entity.Property(e => e.AliasLastName)
                .HasColumnType("character varying")
                .HasColumnName("alias_last_name");
            entity.Property(e => e.AliasMidddleName)
                .HasColumnType("character varying")
                .HasColumnName("alias_midddle_name");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DateOfHistoryVaccine).HasColumnName("date_of_history_vaccine");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.PatientId)
                .ValueGeneratedOnAdd()
                .HasColumnName("patient_id");
            entity.Property(e => e.PatientPrimaryLanguage)
                .HasColumnType("character varying")
                .HasColumnName("patient_primary_language");
            entity.Property(e => e.PatientStatus)
                .HasColumnType("character varying")
                .HasColumnName("patient_status");
            entity.Property(e => e.PatientStatusIndicatorProviderlevel)
                .HasColumnType("character varying")
                .HasColumnName("patient_status_indicator_providerlevel");
            entity.Property(e => e.PatientStatusJuridictionLevel)
                .HasColumnType("character varying")
                .HasColumnName("patient_status_juridiction_level");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.ProtectionIndicator)
                .HasColumnType("character varying")
                .HasColumnName("protection_indicator");
            entity.Property(e => e.ProtectionIndicatorEffectiveDate).HasColumnName("protection_indicator_effective_date");
            entity.Property(e => e.ReminderRecallStatus)
                .HasColumnType("character varying")
                .HasColumnName("reminder_recall_status");
            entity.Property(e => e.ReminderRecallStatusEffectiveDate).HasColumnName("reminder_recall_status_effective_date");
            entity.Property(e => e.ResponsiblePersonFirstName)
                .HasColumnType("character varying")
                .HasColumnName("responsible_person_first_name");
            entity.Property(e => e.ResponsiblePersonLastName)
                .HasColumnType("character varying")
                .HasColumnName("responsible_person_last_name");
            entity.Property(e => e.ResponsiblePersonMidddleName)
                .HasColumnType("character varying")
                .HasColumnName("responsible_person_midddle_name");
            entity.Property(e => e.ResponsiblePersonRelationshipToPatient)
                .HasColumnType("character varying")
                .HasColumnName("responsible_person_relationship_to_patient");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.City).WithMany(p => p.Patients)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("patient_city_id");

            entity.HasOne(d => d.Country).WithMany(p => p.Patients)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("patient_country_id");

            entity.HasOne(d => d.Person).WithMany(p => p.Patients)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("fk_patient_person_id");

            entity.HasOne(d => d.State).WithMany(p => p.Patients)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("patient_state_id");
        });

        modelBuilder.Entity<PatientDuplicateRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patient_duplicate_record_pkey");

            entity.ToTable("patient_duplicate_record");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BirthOrder)
                .HasColumnType("character varying")
                .HasColumnName("birth_order");
            entity.Property(e => e.BirthStateId).HasColumnName("birth_state_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("character varying")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DuplicatePersonId).HasColumnName("duplicate_person_id");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasColumnType("character varying")
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasColumnType("character varying")
                .HasColumnName("middle_name");
            entity.Property(e => e.MotherFirstName)
                .HasColumnType("character varying")
                .HasColumnName("mother_first_name");
            entity.Property(e => e.MotherLastName)
                .HasColumnType("character varying")
                .HasColumnName("mother_last_name");
            entity.Property(e => e.MotherMaidenLastName)
                .HasColumnType("character varying")
                .HasColumnName("mother_maiden_last_name");
            entity.Property(e => e.PersonType)
                .HasColumnType("character varying")
                .HasColumnName("person_type");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<PatientNewRecord>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("patient_new_record");

            entity.Property(e => e.BirthOrder)
                .HasColumnType("character varying")
                .HasColumnName("birth_order");
            entity.Property(e => e.BirthStateId).HasColumnName("birth_state_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("character varying")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.LastName)
                .HasColumnType("character varying")
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasColumnType("character varying")
                .HasColumnName("middle_name");
            entity.Property(e => e.MotherFirstName)
                .HasColumnType("character varying")
                .HasColumnName("mother_first_name");
            entity.Property(e => e.MotherLastName)
                .HasColumnType("character varying")
                .HasColumnName("mother_last_name");
            entity.Property(e => e.MotherMaidenLastName)
                .HasColumnType("character varying")
                .HasColumnName("mother_maiden_last_name");
            entity.Property(e => e.PersonType)
                .HasColumnType("character varying")
                .HasColumnName("person_type");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<PatientStage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patient_stage_pkey");

            entity.ToTable("patient_stage");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.PatientId)
                .HasColumnType("character varying")
                .HasColumnName("patient_id");
            entity.Property(e => e.PatientName)
                .HasColumnType("character varying")
                .HasColumnName("patient_name");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Person_pkey");

            entity.ToTable("person");

            entity.HasIndex(e => e.BirthStateId, "fki_patient_birth_state_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BirthOrder)
                .HasColumnType("character varying")
                .HasColumnName("birth_order");
            entity.Property(e => e.BirthStateId).HasColumnName("birth_state_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("character varying")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.FirstName)
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.LastName)
                .HasColumnType("character varying")
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasColumnType("character varying")
                .HasColumnName("middle_name");
            entity.Property(e => e.MotherFirstName)
                .HasColumnType("character varying")
                .HasColumnName(" mother_first_name");
            entity.Property(e => e.MotherLastName)
                .HasColumnType("character varying")
                .HasColumnName(" mother_last_name");
            entity.Property(e => e.MotherMaidenLastName)
                .HasColumnType("character varying")
                .HasColumnName("mother_maiden_last_name");
            entity.Property(e => e.PersonId)
                .ValueGeneratedOnAdd()
                .HasColumnName("person_id");
            entity.Property(e => e.PersonType)
                .HasColumnType("character varying")
                .HasColumnName("person_type");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.BirthState).WithMany(p => p.People)
                .HasForeignKey(d => d.BirthStateId)
                .HasConstraintName("patient_birth_state_id");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRODUCT_pkey");

            entity.ToTable("product");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CvxCodeId).HasColumnName("cvx_code_id");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.MvxCodeId).HasColumnName("mvx_code_id");
            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("product_id");
            entity.Property(e => e.ProductName)
                .HasColumnType("character varying")
                .HasColumnName("product_name");
            entity.Property(e => e.ProductNameStatus)
                .HasColumnType("character varying")
                .HasColumnName("product_name_status");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Provider_pkey");

            entity.ToTable("provider");

            entity.HasIndex(e => e.FacilityId, "fki_provider_facility_id_fkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ContactNumber)
                .HasColumnType("character varying")
                .HasColumnName("contact_number");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.ProviderId)
                .HasColumnType("character varying")
                .HasColumnName("provider_id");
            entity.Property(e => e.ProviderName)
                .HasColumnType("character varying")
                .HasColumnName("provider_name");
            entity.Property(e => e.ProviderType)
                .HasColumnType("character varying")
                .HasColumnName("provider_type");
            entity.Property(e => e.Specialty)
                .HasColumnType("character varying")
                .HasColumnName("specialty");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.Facility).WithMany(p => p.Providers)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("provider_facility_id_fkey");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shipment_pkey");

            entity.ToTable("shipment");

            entity.HasIndex(e => e.OrderId, "fki_fk_shipment_order_id");

            entity.HasIndex(e => e.ShipmentAddressId, "fki_shipmentaddress_fk_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.ExpectedDeliveryDate).HasColumnName("expected_delivery_date");
            entity.Property(e => e.IsSignatureNeeded)
                .HasColumnType("character varying")
                .HasColumnName("is_signature_needed");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.NoOfPackages)
                .HasColumnType("character varying")
                .HasColumnName("no_of_packages");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PackageSize)
                .HasColumnType("character varying")
                .HasColumnName("package_size");
            entity.Property(e => e.PakegeHeight)
                .HasColumnType("character varying")
                .HasColumnName("pakege_height");
            entity.Property(e => e.PakegeLength)
                .HasColumnType("character varying")
                .HasColumnName("pakege_length");
            entity.Property(e => e.PakegeWidth)
                .HasColumnType("character varying")
                .HasColumnName("pakege_width");
            entity.Property(e => e.ReceiverId)
                .HasColumnType("character varying")
                .HasColumnName("receiver_id");
            entity.Property(e => e.ReceivingHours)
                .HasColumnType("character varying")
                .HasColumnName("receiving_hours");
            entity.Property(e => e.ShipmentAddressId).HasColumnName("shipment_address_id");
            entity.Property(e => e.ShipmentDate).HasColumnName("shipment_date");
            entity.Property(e => e.ShipmentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("shipment_id");
            entity.Property(e => e.SizeUnitOfMesure)
                .HasColumnType("character varying")
                .HasColumnName("size_unit_of_mesure");
            entity.Property(e => e.Status)
                .HasColumnType("character varying")
                .HasColumnName("status");
            entity.Property(e => e.StoringTemparture)
                .HasColumnType("character varying")
                .HasColumnName("storing_temparture");
            entity.Property(e => e.TemperatureUnitOfMeasure)
                .HasColumnType("character varying")
                .HasColumnName("temperature_unit_of_measure");
            entity.Property(e => e.TrackingNumber)
                .HasColumnType("character varying")
                .HasColumnName("tracking_number");
            entity.Property(e => e.TypeOfPackage)
                .HasColumnType("character varying")
                .HasColumnName("type_of_package");
            entity.Property(e => e.TypeOfPackagingMaterial)
                .HasColumnType("character varying")
                .HasColumnName("type_of_packaging_material");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.WeightUnitOfMeasure)
                .HasColumnType("character varying")
                .HasColumnName("weight_unit_of_measure");

            entity.HasOne(d => d.Order).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("fk_shipment_order_id");

            entity.HasOne(d => d.ShipmentAddress).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.ShipmentAddressId)
                .HasConstraintName("shipmentaddress_fk_id");
        });

        modelBuilder.Entity<Site>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Site_pkey");

            entity.ToTable("site");

            entity.HasIndex(e => e.FacilityId, "fki_facilities_id");

            entity.HasIndex(e => e.AddressId, "fki_fk_site_address_id");

            entity.HasIndex(e => e.FacilityId, "fki_fk_site_facility_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.IsImmunizationSite).HasColumnName("is_immunization_site");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.ParentSite)
                .HasColumnType("character varying")
                .HasColumnName("parent_site");
            entity.Property(e => e.SiteContactPerson)
                .HasColumnType("character varying")
                .HasColumnName("site_contact_person");
            entity.Property(e => e.SiteId)
                .HasColumnType("character varying")
                .HasColumnName("site_id");
            entity.Property(e => e.SiteName)
                .HasColumnType("character varying")
                .HasColumnName("site_name");
            entity.Property(e => e.SitePinNumber)
                .HasColumnType("character varying")
                .HasColumnName("site_pin_number");
            entity.Property(e => e.SiteType)
                .HasColumnType("character varying")
                .HasColumnName("site_type");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.Address).WithMany(p => p.Sites)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("fk_site_address_id");

            entity.HasOne(d => d.Facility).WithMany(p => p.Sites)
                .HasForeignKey(d => d.FacilityId)
                .HasConstraintName("fk_site_facility_id");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("States_pkey");

            entity.ToTable("states");

            entity.HasIndex(e => e.CountryId, "fki_c");

            entity.HasIndex(e => e.CountryId, "fki_fk_countryids");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.StateCode)
                .HasColumnType("character varying")
                .HasColumnName("state_code");
            entity.Property(e => e.StateId)
                .ValueGeneratedOnAdd()
                .HasColumnName("state_id");
            entity.Property(e => e.StateName)
                .HasColumnType("character varying")
                .HasColumnName("state_name");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("fk_country_id");
        });

        modelBuilder.Entity<StatesMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("states_master");

            entity.Property(e => e.StateCode)
                .HasColumnType("character varying")
                .HasColumnName("state_code");
            entity.Property(e => e.StateName)
                .HasColumnType("character varying")
                .HasColumnName("state_name");
        });

        modelBuilder.Entity<TermsCondition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("terms_conditions_pkey");

            entity.ToTable("terms_conditions");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.TermsConditions)
                .HasColumnType("character varying")
                .HasColumnName("terms_conditions");
            entity.Property(e => e.TermsConditionsId)
                .ValueGeneratedOnAdd()
                .HasColumnName("terms_conditions_id");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<TestAble>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("test_able_pkey");

            entity.ToTable("test_able");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.PersonId, "fki_fk_user_person_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Designation)
                .HasColumnType("character varying")
                .HasColumnName("designation");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("character varying")
                .HasColumnName("image_url");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.SequenceId)
                .ValueGeneratedOnAdd()
                .HasColumnName("sequence_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.UserId)
                .HasColumnType("character varying")
                .HasColumnName("user_id");
            entity.Property(e => e.UserType)
                .HasColumnType("character varying")
                .HasColumnName("user_type");

            entity.HasOne(d => d.Person).WithMany(p => p.Users)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("fk_user_person_id");
        });

        modelBuilder.Entity<VaccinePrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Vaccine_price_pkey");

            entity.ToTable("vaccine_price");

            entity.HasIndex(e => e.CvxId, "fki_cvx_fk_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Brandname)
                .HasColumnType("character varying")
                .HasColumnName("brandname");
            entity.Property(e => e.ContractEndDate)
                .HasColumnType("character varying")
                .HasColumnName("contract_end_date");
            entity.Property(e => e.ContractNumber)
                .HasColumnType("character varying")
                .HasColumnName("contract_number");
            entity.Property(e => e.CostPerDose)
                .HasColumnType("character varying")
                .HasColumnName("cost_per_dose");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CvxId).HasColumnName("cvx_id");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.Manufacturer)
                .HasColumnType("character varying")
                .HasColumnName("manufacturer");
            entity.Property(e => e.Packaging)
                .HasColumnType("character varying")
                .HasColumnName("packaging");
            entity.Property(e => e.PriceId)
                .ValueGeneratedOnAdd()
                .HasColumnName("price_id");
            entity.Property(e => e.PrivateSectorCostPerDose)
                .HasColumnType("character varying")
                .HasColumnName("private_sector_cost_per_dose");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.Cvx).WithMany(p => p.VaccinePrices)
                .HasForeignKey(d => d.CvxId)
                .HasConstraintName("cvx_fk_id");
        });

        modelBuilder.Entity<VaccinePriceDoNotUse>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Vaccine-price_do_not_use");

            entity.Property(e => e.Brandname).HasColumnType("character varying");
            entity.Property(e => e.CdccostDose)
                .HasColumnType("character varying")
                .HasColumnName("CDCCost-Dose");
            entity.Property(e => e.ContractEndDate)
                .HasColumnType("character varying")
                .HasColumnName("Contract End Date");
            entity.Property(e => e.ContractNumber)
                .HasColumnType("character varying")
                .HasColumnName("Contract Number");
            entity.Property(e => e.Manufacturer).HasColumnType("character varying");
            entity.Property(e => e.Ndc)
                .HasColumnType("character varying")
                .HasColumnName("NDC");
            entity.Property(e => e.Packaging).HasColumnType("character varying");
            entity.Property(e => e.PrivateSectorCostDose)
                .HasColumnType("character varying")
                .HasColumnName("Private Sector Cost- Dose");
            entity.Property(e => e.Vaccine)
                .HasColumnType("character varying")
                .HasColumnName("vaccine");
        });

        modelBuilder.Entity<VaccinepriceBkp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("vaccineprice_bkp");

            entity.Property(e => e.Brandname)
                .HasColumnType("character varying")
                .HasColumnName("brandname");
            entity.Property(e => e.ContractEndDate)
                .HasColumnType("character varying")
                .HasColumnName("contract_end_date");
            entity.Property(e => e.ContractNumber)
                .HasColumnType("character varying")
                .HasColumnName("contract_number");
            entity.Property(e => e.CostPerDose)
                .HasColumnType("character varying")
                .HasColumnName("cost_per_dose");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.CvxId).HasColumnName("cvx_id");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.Manufacturer)
                .HasColumnType("character varying")
                .HasColumnName("manufacturer");
            entity.Property(e => e.NdcId).HasColumnName("ndc_id");
            entity.Property(e => e.Packaging)
                .HasColumnType("character varying")
                .HasColumnName("packaging");
            entity.Property(e => e.PriceId).HasColumnName("price_id");
            entity.Property(e => e.PrivateSectorCostPerDose)
                .HasColumnType("character varying")
                .HasColumnName("private_sector_cost_per_dose");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
