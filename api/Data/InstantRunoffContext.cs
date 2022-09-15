using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using shared;

namespace election.Data
{
    public partial class InstantRunoffContext : DbContext
    {
        public InstantRunoffContext()
        {
        }

        public InstantRunoffContext(DbContextOptions<InstantRunoffContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ballot> Ballots { get; set; } = null!;
        public virtual DbSet<BallotPref> BallotPrefs { get; set; } = null!;
        public virtual DbSet<Candidate> Candidates { get; set; } = null!;
        public virtual DbSet<CandidateOffice> CandidateOffices { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<County> Counties { get; set; } = null!;
        public virtual DbSet<Office> Offices { get; set; } = null!;
        public virtual DbSet<State> States { get; set; } = null!;

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ballot>(entity =>
            {
                entity.ToTable("ballot", "iro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CastTimestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("cast_timestamp");

                entity.Property(e => e.Precinctinfo).HasColumnName("precinctinfo");

                entity.Property(e => e.VoterId).HasColumnName("voter_id");
            });

            modelBuilder.Entity<BallotPref>(entity =>
            {
                entity.ToTable("ballot_pref", "iro");

                entity.HasIndex(e => new { e.BallotId, e.OfficeId, e.PreferenceNum }, "ballot_pref_pref_uix")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BallotId).HasColumnName("ballot_id");

                entity.Property(e => e.CandidateOfficeId).HasColumnName("candidate_office_id");

                entity.Property(e => e.OfficeId).HasColumnName("office_id");

                entity.Property(e => e.PreferenceNum).HasColumnName("preference_num");

                entity.HasOne(d => d.Ballot)
                    .WithMany(p => p.BallotPrefs)
                    .HasForeignKey(d => d.BallotId)
                    .HasConstraintName("ballot_pref_ballot_fk");

                entity.HasOne(d => d.CandidateOffice)
                    .WithMany(p => p.BallotPrefs)
                    .HasForeignKey(d => d.CandidateOfficeId)
                    .HasConstraintName("ballot_pref_candidate_office_fk");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.BallotPrefs)
                    .HasForeignKey(d => d.OfficeId)
                    .HasConstraintName("ballot_pref_office_fk");
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("candidate", "iro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CandidateEmail)
                    .HasMaxLength(200)
                    .HasColumnName("candidate_email");

                entity.Property(e => e.CandidateName)
                    .HasMaxLength(200)
                    .HasColumnName("candidate_name");

                entity.Property(e => e.CandidatePhone)
                    .HasMaxLength(80)
                    .HasColumnName("candidate_phone");

                entity.Property(e => e.CandidatePhoto).HasColumnName("candidate_photo");
            });

            modelBuilder.Entity<CandidateOffice>(entity =>
            {
                entity.ToTable("candidate_office", "iro");

                entity.HasIndex(e => new { e.OfficeId, e.CandidateId }, "office_id_candidate_id_uix")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CandidateId).HasColumnName("candidate_id");

                entity.Property(e => e.EliminatedTf).HasColumnName("eliminated_tf");

                entity.Property(e => e.FiledDate).HasColumnName("filed_date");

                entity.Property(e => e.OfficeId).HasColumnName("office_id");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateOffices)
                    .HasForeignKey(d => d.CandidateId)
                    .HasConstraintName("candidate_office_candidate_fk");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.CandidateOffices)
                    .HasForeignKey(d => d.OfficeId)
                    .HasConstraintName("candidate_office_office_fk");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("city", "iro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityDescription)
                    .HasMaxLength(80)
                    .HasColumnName("city_description");

                entity.Property(e => e.CityName)
                    .HasMaxLength(80)
                    .HasColumnName("city_name");

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(200)
                    .HasColumnName("contact_email");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(80)
                    .HasColumnName("contact_name");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(80)
                    .HasColumnName("contact_phone");

                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(80)
                    .HasColumnName("contact_title");
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.ToTable("county", "iro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(200)
                    .HasColumnName("contact_email");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(80)
                    .HasColumnName("contact_name");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(80)
                    .HasColumnName("contact_phone");

                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(80)
                    .HasColumnName("contact_title");

                entity.Property(e => e.CountyDescription)
                    .HasMaxLength(80)
                    .HasColumnName("county_description");

                entity.Property(e => e.CountyName)
                    .HasMaxLength(80)
                    .HasColumnName("county_name");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("office", "iro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.CountyId).HasColumnName("county_id");

                entity.Property(e => e.OfficeName)
                    .HasMaxLength(80)
                    .HasColumnName("office_name");

                entity.Property(e => e.PositionsNum).HasColumnName("positions_num");

                entity.Property(e => e.StateId).HasColumnName("state_id");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("office_city_fk");

                entity.HasOne(d => d.County)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.CountyId)
                    .HasConstraintName("office_county_fk");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.StateId)
                    .HasConstraintName("office_state_fk");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("state", "iro");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(200)
                    .HasColumnName("contact_email");

                entity.Property(e => e.ContactName)
                    .HasMaxLength(80)
                    .HasColumnName("contact_name");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(80)
                    .HasColumnName("contact_phone");

                entity.Property(e => e.ContactTitle)
                    .HasMaxLength(80)
                    .HasColumnName("contact_title");

                entity.Property(e => e.StateDescription)
                    .HasMaxLength(80)
                    .HasColumnName("state_description");

                entity.Property(e => e.StateName)
                    .HasMaxLength(80)
                    .HasColumnName("state_name");
            });

            modelBuilder.HasSequence("voter_id_seq", "iro").StartsAt(111);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
