using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class RandomNumberGenerator
    {
        private Random VisitRND;
        private Random PurchaseRND;
        private Random TrafficSourceRND;
        private Random SessionDurationClassRND;
        private Random SessionDurationRND1;
        private Random SessionDurationRND2;
        private Random SessionDurationRND3;
        private Random PageCountRND1;
        private Random PageCountRND2;
        private Random PageCountRND3;
        private Random CustomerTypeRND;
        private Random AmountRND;

        public RandomNumberGenerator()
        {
            VisitRND = new Random();
            PurchaseRND = new Random();
            TrafficSourceRND = new Random();
            SessionDurationClassRND = new Random();
            SessionDurationRND1 = new Random();
            SessionDurationRND2 = new Random();
            SessionDurationRND3 = new Random();
            PageCountRND1 = new Random();
            PageCountRND2 = new Random();
            PageCountRND3 = new Random();
            CustomerTypeRND = new Random();
            AmountRND = new Random();
        }

        public int GetNextVisitCount()
        {
            return VisitRND.Next(0, 15);
        }

        public bool IsPurchased()
        {
            int PurchaseRandomValue = PurchaseRND.Next(1, 100);
            bool IsPurchasedFlag;

            if (PurchaseRandomValue < 21)
            {
                IsPurchasedFlag = true;
            }
            else
            {
                IsPurchasedFlag = false;
            }
            return IsPurchasedFlag;
        }

        public string GetTrafficSource()
        {
            int TrafficSourceRandomValue = TrafficSourceRND.Next(1, 100);
            string TrafficSource = string.Empty;

            if (TrafficSourceRandomValue > 75)
            {
                TrafficSource = "Social";
            }
            else if (TrafficSourceRandomValue > 55)
            {
                TrafficSource = "Referral";
            }
            else if (TrafficSourceRandomValue > 45)
            {
                TrafficSource = "Display";
            }
            else if (TrafficSourceRandomValue > 34)
            {
                TrafficSource = "Direct";
            }
            else if (TrafficSourceRandomValue > 24)
            {
                TrafficSource = "Organic";
            }
            else
            {
                TrafficSource = "Paid";
            }

            return TrafficSource;
        }

        public int GetSessionDuration()
        {
            int SessionDurationClassValue = SessionDurationClassRND.Next(1, 100);
            int SessionDurationValue = 0;

            if (SessionDurationClassValue > 50)
            {
                SessionDurationValue = SessionDurationRND1.Next(1, 20);
            }
            else if (SessionDurationClassValue > 20)
            {
                SessionDurationValue = SessionDurationRND2.Next(21, 35);
            }
            else if (SessionDurationClassValue > 0)
            {
                SessionDurationValue = SessionDurationRND3.Next(35, 45);
            }

            return SessionDurationValue;
        }

        public int GetPageCount(int SessionDuration)
        {
            int PageCount = 0;
            if (SessionDuration > 35)
            {
                PageCount = PageCountRND1.Next(36, 50);
            }
            else if (SessionDuration > 21)
            {
                PageCount = PageCountRND1.Next(21, 35);
            }
            else if (SessionDuration > 0)
            {
                PageCount = PageCountRND1.Next(1, 20);
            }

            return PageCount;

        }

        public string GetCustomerType()
        {
            string CustomerType = string.Empty;

            int CustomerTypeRNDValue = CustomerTypeRND.Next(1, 100);

            if (CustomerTypeRNDValue > 65)
            {
                CustomerType = "New";
            }
            else
            {
                CustomerType = "Returning";
            }

            return CustomerType;
        }

        public int GetAmount()
        {
            return AmountRND.Next(2, 7);
        }

    }


}
