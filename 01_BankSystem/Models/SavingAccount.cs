namespace BankSystem.Models
{
    public class SavingAccount : Account
    {
        public double Rate { get; set; }

        public void AddRate()
        {
            this.Ballance *= (decimal)this.Rate;
        }
    }
}