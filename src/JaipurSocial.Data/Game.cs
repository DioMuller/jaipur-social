//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JaipurSocial.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Game
    {
        public int Id { get; set; }
        public bool EnemyTurn { get; set; }
        public string OnTable { get; set; }
        public string OnDeck { get; set; }
        public string Resources { get; set; }
        public int ChallengerId { get; set; }
        public string ChallengerHand { get; set; }
        public int ChallengerPoints { get; set; }
        public int ChallengerCamels { get; set; }
        public int EnemyId { get; set; }
        public string EnemyHand { get; set; }
        public int EnemyPoints { get; set; }
        public int EnemyCamels { get; set; }
    }
}
