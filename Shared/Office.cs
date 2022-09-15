using System;
using System.Collections.Generic;


namespace shared
{
    public partial class Office
    {
        public Office()
        {
            BallotPrefs = new HashSet<BallotPref>();
            CandidateOffices = new HashSet<CandidateOffice>();
        }

        public int Id { get; set; }
        public string? OfficeName { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? PositionsNum { get; set; }

        public virtual City? City { get; set; }
        public virtual County? County { get; set; }
        public virtual State? State { get; set; }
        public virtual ICollection<BallotPref> BallotPrefs { get; set; }
        public virtual ICollection<CandidateOffice> CandidateOffices { get; set; }
    }
}
