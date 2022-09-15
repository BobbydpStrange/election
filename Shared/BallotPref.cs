using System;
using System.Collections.Generic;


namespace shared
{
    public partial class BallotPref
    {
        public int Id { get; set; }
        public int? BallotId { get; set; }
        public int? OfficeId { get; set; }
        public int? PreferenceNum { get; set; }
        public int? CandidateOfficeId { get; set; }

        public virtual Ballot? Ballot { get; set; }
        public virtual CandidateOffice? CandidateOffice { get; set; }
        public virtual Office? Office { get; set; }
    }
}
