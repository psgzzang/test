using Newtonsoft.Json;
using SPM;
using SPM.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace VoidTest
{
    public partial class Form1 : Form
    {
        private Request _request;
        private string _hostOrderID;
        private Order _oldOrder;
        private AnytimeScanEventNotifier atse;
        private Request _oldrequest;

        private Class1 a;
        public Form1()
        {
            InitializeComponent();
            //
            //SPMPinpad.Devices.CardScanner.AnytimeCardScanner.KeyPress += HookManager_KeyPress;
            //HookManager.KeyPress += HookManager_KeyPress;
            //SPMPinpad.Devices.CardScanner.AnytimeScanner cardscanner = SPMPinpad.Devices.CardScanner.AnytimeScanner.GetInstance();
            //cardscanner.Enabled = true;
            atse = AnytimeScanEventNotifier.Instance();
            atse.ScanEventWithDetails += ScanEventReceived;
            //atse.DirectBalanceInquiryEnabled = true;

            //
            //  cboTenderType.DataSource = TenderType.ToList();
            //  cboTenderType.DisplayMember = "Name";
            

        }


        private void ScanEventReceived(object sender, AnytimeScanEventArgs args)
        {
            atse.IsEnabled = false;
            MessageBox.Show(args.Card.AccountIDSanitized);
            atse.IsEnabled = true;
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Console.WriteLine(string.Format("KeyPress - {0}\n", e.KeyChar));
        }

        private class Test
        {
            public DateTime Date { get; set; }
            public Boolean boolvalue { get; set; }
        }

        //Placing Order (Cash)
        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            a = new Class1();
            //List<Test> testlist = new List<Test>();

            _request = new Request();
            

            //Certificates certs = new Certificates();
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 2, 1, 1, 1), IsForfeitable = false, IsSelectedByCustomer = false });
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 3, 1, 1, 1), IsForfeitable = false, IsSelectedByCustomer = true, MDMID = "11111" });
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 1, 1, 1, 1), IsForfeitable = false, IsSelectedByCustomer = false, MDMID = "11111" });
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 4, 1, 1, 1), IsForfeitable = false, IsSelectedByCustomer = true, MDMID = "11311" });
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 5, 1, 1, 1), IsForfeitable = false, IsSelectedByCustomer = true, MDMID = "11111" });
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 6, 1, 1, 1), IsForfeitable = false, IsSelectedByCustomer = true, MDMID = "11111" });
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 9, 1, 1, 1), IsForfeitable = false, IsSelectedByCustomer = true, MDMID = "11111" });
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 8, 1, 1, 1), IsForfeitable = false, IsSelectedByCustomer = true, MDMID = "11111" });
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 7, 1, 1, 1), IsForfeitable = false, IsSelectedByCustomer = true, MDMID = "11111" });
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 11, 1, 1, 1), IsForfeitable = true, IsSelectedByCustomer = true, MDMID = "11111" });
            //certs.AddObject(new Certificate { EndDate = new DateTime(2016, 6, 10, 1, 1, 1), IsForfeitable = true, IsSelectedByCustomer = false, MDMID = "11111" });

            //List<Certificate> certstoPay = certs.FindAllDesiredCertificatesToUse(3.00m, Currency.USD);

            //Coupons coupons = new Coupons();
            //coupons.AddObject(new Coupon { CouponRawData = "111111111111111111111111111111111", MDMID = "11111" });
            //coupons.AddObject(new Coupon { CouponRawData = "111111111111111111111111111111112", MDMID = "11111" });
            //coupons.AddObject(new Coupon { CouponRawData = "111111111111111111111111111111113", MDMID = "11111" });
            //coupons.AddObject(new Coupon { CouponRawData = "111111111111111111111111111111114", MDMID = "11111" });

            _request.ClerkID = "1";
            _request.PosVersion = "1.0";
            _request.PosStoreNumber = "cad100"; //storenumber
            
            SPMProfiles.ActiveProfile.IsProduction = false;

            VoidData vd = new VoidData();
            vd.MDMID = "abcde";
            vd.HostOrderID = "1234";
            vd.OriginalTransactionDate = DateTime.Now;
            vd.TenderAccountID = "abcd1234123412345";
            vd.Processor = SpmEnums.Processor.VP3;

            VoidData vd2 = new VoidData();
            vd2.MDMID = "abcde";
            vd2.HostOrderID = "1234";
            vd2.OriginalTransactionDate = DateTime.Now;
            vd2.TenderAccountID = "abcd1234123412345";
            vd2.Processor = SpmEnums.Processor.SnapLogic;

            VoidDataCollection vds = new VoidDataCollection();
            vds.Add(vd);
            vds.Add(vd2);

            //var json = vds.GetVoidReferenceNumber();
            //VoidDataCollection vdsout = new VoidDataCollection();
            //vdsout.SetVoidReferenceNumber(json);

            ////var testjson = "[{\"Processor\":1,\"OriginalTransactionDate\":\"2016-12-21T11:12:37.40371-08:00\",\"HostOrderID\":\"1234\",\"TenderAccountID\":\"abcd1234123412345\",\"MDMID\":\"abcde\"},{\"Processor\":0,\"OriginalTransactionDate\":\"2016-12-21T11:12:37.4042104-08:00\",\"HostOrderID\":\"1234\",\"TenderAccountID\":\"abcd1234123412345\",\"MDMID\":\"abcde\"}]";
            //var oldvoidref = "1234sdfsdf";
            //vdsout.SetVoidReferenceNumber(oldvoidref);

            //Card testcard = new Card();
            //testcard.VoidReferenceNumber = oldvoidref;

            //string teststring = testcard.VoidReferenceNumber;

            SPMPinpad.Devices.Verifone.VX820.GetInstance().Open();

            //Add reward card to earn points
            //_request.AddCardPromo(); // Or if AnytimeScan used, pass card object like request.AddCardPromo(Card scannedCard)

            Order order = _request.Order;
            order.TransNumber = "123";
            order.OrderDate = DateTime.Now;
            
            //Buying merchandise
            LineItem lineItem1 = order.LineItems.NewLineItem();
            lineItem1.Amount = 1;
            lineItem1.PLU = "123";
            lineItem1.Currency = Currency.GBP;
            lineItem1.Description = "test";
            lineItem1.AddOns.Add("key", "name", "value");
            lineItem1.LineItemType = SpmEnums.LineItemTypes.Merchandise;
            order.LineItems.AddLineItem(lineItem1);

            ////Buying Gift card
            //LineItem lineItem2 = order.LineItems.NewLineItem();
            //lineItem2.Amount = 1;
            //lineItem2.PLU = "123";
            //lineItem2.Currency = Currency.GBP;
            //lineItem2.AddOns.Add("key", "name", "value");
            //lineItem2.LineItemType = SpmEnums.LineItemTypes.AddGift; // For buying gift card
            //Card giftcard = new Card();
            //giftcard.GetSubwayCardData(SpmEnums.TransactionTypes.GenericSubwayCard);
            //lineItem2.SetCard(giftcard);
            //order.LineItems.AddLineItem(lineItem2);

            ////Add Credit Card Payment
            //Payment payment = order.Payments.NewPayment();
            //payment.TenderType = TenderType.CreditCard;
            //payment.Currency = Currency.GBP;
            //payment.Amount = 3.00;
            //if (!payment.GetCardData(false)) // set IsVoid parameter to False
            //{
            //    MessageBox.Show("Failed");
            //    return;
            //}

            ////Add gift Card Payment
            //Payment payment = order.Payments.NewPayment();
            //payment.TenderType = TenderType.GiftCard;
            //payment.Currency = Currency.GBP;
            //payment.Amount = 1.00;
            //if (!payment.GetCardData(false)) // set IsVoid parameter to False
            //{
            //    MessageBox.Show("Failed");
            //    return;
            //}
            //order.Payments.AddPayment(payment);

            //Add cash Payment
            Payment payment = order.Payments.NewPayment();
            payment.TenderType = TenderType.Cash;
            payment.Amount = 2.00;
            payment.Currency = Currency.USD;
            order.Payments.AddPayment(payment);

            //redeem coupon

            //foreach (Coupon c in coupons)
            //{
            //    Payment couponpayment = order.Payments.NewPayment();
            //    couponpayment.TenderType = TenderType.CouponVoucher;
            //    couponpayment.SetCoupon(c);
            //    order.Payments.AddPayment(couponpayment); //You can add multiple payment objects with other coupons.
            //}

            //foreach (Certificate c in certs)
            //{
            //    Payment certPayment = order.Payments.NewPayment();
            //    certPayment.TenderType = TenderType.CouponVoucher;
            //    certPayment.SetCertificate(c);
            //    order.Payments.AddPayment(certPayment); //You can add multiple payment objects with other coupons.
            //}
            
            if (_request.ProcessOrder())
            {
                _hostOrderID = _request.Order.OrderID;
                MessageBox.Show(SPM.Resources.Strings.VoidTest_ResultStateOrderSuccess + _request.Response.ResultStatus.ToString() + "\n " + _request.Response.get_ReceiptText());

                _oldOrder = order;
                _oldrequest = _request;
            }
            else
            {
                MessageBox.Show(_request.Error.ErrorMessage);
            }
        }


        private void btnProcessGiftCard_Click(object sender, EventArgs e)
        {
            _request = new Request();
            
            _request.ClerkID = "1";
            _request.PosVersion = "1";
            _request.PosStoreNumber = "cad100"; //storenumber

            Order order = _request.Order;
            order.Description = "GiftCard";
            order.TransNumber = "123";
            order.OrderDate = DateTime.Now;

            //Buying merchandise
            LineItem lineItem1 = order.LineItems.NewLineItem();
            lineItem1.Amount = 1;
            lineItem1.PLU = "123";
            lineItem1.Currency = Currency.USD;
            lineItem1.Description = "test";
            lineItem1.AddOns.Add("key", "name", "value");
            lineItem1.LineItemType = SpmEnums.LineItemTypes.Merchandise;
            order.LineItems.AddLineItem(lineItem1);

            //Add Giftcard Payment
            Payment payment = order.Payments.NewPayment();
            payment.TenderType = TenderType.GiftCard;
            payment.Amount = 2.00;
            payment.Currency = Currency.USD;
            if (!payment.GetCardData(false)) // set IsVoid parameter to False
            {
                MessageBox.Show("Failed");
                return;
            }
            order.Payments.AddPayment(payment);

            if (_request.ProcessOrder())
            {
                _hostOrderID = _request.Order.OrderID;
                MessageBox.Show(SPM.Resources.Strings.VoidTest_ResultStateOrderSuccess + _request.Response.ResultStatus.ToString() + "\n " + _request.Response.get_ReceiptText());

                _oldOrder = order;
                _oldrequest = _request;
            }
            else
            {
                MessageBox.Show(_request.Error.ErrorMessage);
            }


        }
        private void btnProcessCreditCard_Click(object sender, EventArgs e)
        {
            _request = new Request();
            _request.ClerkID = "1";
            _request.PosVersion = "1";
            _request.PosStoreNumber = "cad100"; //storenumber

            Order order = _request.Order;
            order.Description = "CreditCard";
            order.TransNumber = "1";
            order.OrderDate = DateTime.Today;

            //Buying merchandise
            LineItem lineItem1 = order.LineItems.NewLineItem();
            lineItem1.Amount = 1;
            lineItem1.PLU = "123";
            lineItem1.Currency = Currency.USD;
            lineItem1.Description = "test";
            lineItem1.AddOns.Add("key", "name", "value");
            lineItem1.LineItemType = SpmEnums.LineItemTypes.Merchandise;
            order.LineItems.AddLineItem(lineItem1);

            //Add Creditcard Payment
            Payment payment = order.Payments.NewPayment();
            payment.Currency = Currency.USD;
            payment.TenderType = TenderType.CreditCard;
            payment.Amount = 2.5;
            if(!payment.GetCardData(false))
            {
                MessageBox.Show("Failed");
                return;
            }

            order.Payments.AddPayment(payment);

            if (_request.ProcessOrder())
            {
                _hostOrderID = _request.Order.OrderID;
                MessageBox.Show(SPM.Resources.Strings.VoidTest_ResultStateOrderSuccess + _request.Response.ResultStatus.ToString() + "\n " + _request.Response.get_ReceiptText());

                _oldOrder = order;
                _oldrequest = _request;
            }
            else
            {
                MessageBox.Show(_request.Error.ErrorMessage);
            }



        }

        //If SPM is configured to use Standalone Payment Processor, This will do refund instead of voiding
        private void btnVoid_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_hostOrderID))
            {
                MessageBox.Show("Host Order ID not set.");
                return;
            }

            _request = new Request();
            _request.Clear();
            _request.ClerkID = "1";
            _request.PosVersion = "1.0";
            _request.PosStoreNumber = "crdnt400";

            Order voidOrder = _request.Order;

            //to void addpoint

            foreach (Card oldcard in _oldrequest.Cards)
            {
                if (oldcard.IsPromo) //there should be only one promo card in cards collection
                {
                    Card rewardcard = new Card();
                    rewardcard.GetSubwayCardData(SpmEnums.TransactionTypes.VoidPointsSubwayCard); // to get the card account ID which might not be saved in SubwayPOS
                    rewardcard.VoidReferenceNumber = oldcard.VoidReferenceNumber;
                    _request.AddCardPromo(rewardcard);
                }
            }

            //build line Item

            foreach (LineItem oldLineItem in _oldOrder.LineItems)
            {
                LineItem voidLineItem = voidOrder.LineItems.NewLineItem();
                voidLineItem.Amount = oldLineItem.Amount;
                voidLineItem.PLU = oldLineItem.PLU;
                voidLineItem.Currency = oldLineItem.Currency;
                //this may be ommitted if POS not uses
                foreach (AddOn oldAddon in oldLineItem.AddOns)
                {
                    voidLineItem.AddOns.Add(oldAddon.Key, oldAddon.Name, oldAddon.Value);
                }

                if (oldLineItem.LineItemType == SpmEnums.LineItemTypes.AddGift)
                {
                    voidLineItem.LineItemType = SpmEnums.LineItemTypes.AddGift;
                    Card voidgiftcard = new Card();
                    voidgiftcard.GetSubwayCardData(SpmEnums.TransactionTypes.GenericSubwayCard);
                    voidgiftcard.VoidReferenceNumber = oldLineItem.Card.VoidReferenceNumber;
                    voidLineItem.SetCard(voidgiftcard);
                }
                else if (oldLineItem.LineItemType == SpmEnums.LineItemTypes.Merchandise)
                {
                    voidLineItem.LineItemType = SpmEnums.LineItemTypes.Merchandise;
                }
                else
                {
                    voidLineItem.LineItemType = SpmEnums.LineItemTypes.Tax;
                }

                voidOrder.LineItems.AddLineItem(voidLineItem);
            }

            // build payments

            foreach (Payment oldPayment in _oldOrder.Payments)
            {
                Payment voidPayment = voidOrder.Payments.NewPayment();
                voidPayment.Currency = oldPayment.Currency;
                voidPayment.Amount = oldPayment.Amount;
                voidPayment.TenderType = oldPayment.TenderType;
                if (oldPayment.TenderType == TenderType.Cash)
                {
                }
                else if (oldPayment.TenderType == TenderType.CouponVoucher)
                {
                    Coupon voidcoupon = new Coupon();
                    voidcoupon.VoidReferenceNumber = oldPayment.Coupon.VoidReferenceNumber;
                    voidPayment.SetCoupon(voidcoupon);
                }
                else
                {
                    //credit card, debitcard , subway reward card for redeeming points, subway gift card set all here
                    if (!voidPayment.GetCardData(true)) // set IsVoid parameter to TRUE
                    {
                        MessageBox.Show("Failed");
                    }

                    if (oldPayment.TenderType == TenderType.GiftCard || oldPayment.TenderType == TenderType.RewardsCard)
                    {
                        if (voidPayment.Card.AccountIDLast4 != oldPayment.Card.AccountIDLast4)
                        {
                            //error handle here for only subway reward/gift cards incase there were multiple gift card used. you may wanna re prompt getcarddata or display error message
                            MessageBox.Show("Card number in not matched");
                        }
                    }

                    voidPayment.Card.VoidReferenceNumber = oldPayment.Card.VoidReferenceNumber;
                }

                voidOrder.Payments.AddPayment(voidPayment);
            }

            if (_request.VoidOrder())
            {
                MessageBox.Show(SPM.Resources.Strings.VoidTest_ResultStateVoidSuccess + _request.Response.ResultStatus.ToString() + "\n " + _request.Response.get_ReceiptText());
            }
            else
            {
                MessageBox.Show("Failed");
            }
        }

        //Get Coupons and Certificates data from a customer card.
        private void btnGetCardData_Click(object sender, EventArgs e)
        {
            _request = new Request();
            _request.ClerkID = "1";
            _request.PosVersion = "1.0";
            _request.PosStoreNumber = "store123"; //storenumber


            Card subcard = new Card();
            if (_request.BalanceInquiry(ref subcard) && _request.Response.ResultStatus == SpmEnums.ResultStatuses.OK)
            {
                MessageBox.Show("Balance Inquiry success. Result State: " + _request.Response.ResultStatus.ToString() + "\n " + _request.Response.get_ReceiptText());
                //This example used Windows.Forms.ComboBox instead of buttons.
                if (subcard.HasCoupon)
                {
                    cboCoupons.DataSource = subcard.Coupons;
                    cboCoupons.DisplayMember = "Title";
                }
                else
                {
                    //No coupons availabe for this card.
                }

                if (subcard.HasCertificates)
                {
                    cboCertificates.DataSource = subcard.Certificates;

                    cboCertificates.DisplayMember = "SerialNumber";
                }

                else
                {
                    //No Certificates availabe for this card.
                }
            }
            else
            {
                //BalanceInquiry Failed
                MessageBox.Show(_request.Error.ErrorMessage);
            }
        }

        private void btnProcessCoupon_Click(object sender, EventArgs e)
        {
            Coupon coupon = (Coupon)cboCoupons.SelectedItem;
            Order order = _request.Order;
            order.TransNumber = "123";
            order.OrderDate = DateTime.Now;

           

            Payment pay = order.Payments.NewPayment();
            pay.TenderType = TenderType.CouponVoucher;
            pay.SetCoupon(coupon);
            order.Payments.AddPayment(pay); //You can add multiple payment objects with other coupons.

            if (_request.ProcessOrder() && _request.Response.ResultStatus == SpmEnums.ResultStatuses.OK)
            {
                _hostOrderID = _request.Order.OrderID;
                MessageBox.Show(SPM.Resources.Strings.VoidTest_ResultStateOrderSuccess + _request.Response.ResultStatus.ToString() + "\n " + _request.Response.get_ReceiptText());
            }
            else
            {
                MessageBox.Show(_request.Error.ErrorMessage);
            }
        }
        private void btnProcessCertificate_Click(object sender, EventArgs e)
        {
            Certificate certificate = (Certificate)cboCertificates.SelectedItem;
            Order order = _request.Order;
            order.TransNumber = "123";
            order.OrderDate = DateTime.Now;

            LineItem lineItem1 = order.LineItems.NewLineItem();
            lineItem1.Amount = 1;
            lineItem1.PLU = "123";
            lineItem1.Currency = Currency.USD;
            lineItem1.Description = "test";
            lineItem1.AddOns.Add("key", "name", "value");
            lineItem1.LineItemType = SpmEnums.LineItemTypes.Merchandise;
            order.LineItems.AddLineItem(lineItem1);

            Payment pay = order.Payments.NewPayment();
            pay.TenderType = TenderType.Certificate;
            pay.SetCertificate(certificate);
            order.Payments.AddPayment(pay); //You can add multiple payment objects with other certificates

            if (_request.ProcessOrder() && _request.Response.ResultStatus == SpmEnums.ResultStatuses.OK)
            {
                _hostOrderID = _request.Order.OrderID;
                MessageBox.Show(SPM.Resources.Strings.VoidTest_ResultStateOrderSuccess + _request.Response.ResultStatus.ToString() + "\n " + _request.Response.get_ReceiptText());
            }
            else
            {
                MessageBox.Show(_request.Error.ErrorMessage);
            }
        }

        private void AnytimeScanCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AnytimeScanCheckBox.Checked && !atse.IsEnabled)
            {
                atse.IsEnabled = true;
            }
            else if (!AnytimeScanCheckBox.Checked && atse.IsEnabled)
            {
                atse.IsEnabled = false;
            }
        }

        private void DirectBalanceInquiryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (DirectBalanceInquiryCheckBox.Checked && !atse.DirectBalanceInquiryEnabled)
            {
                atse.DirectBalanceInquiryEnabled = true;
            }
            else if (!DirectBalanceInquiryCheckBox.Checked && atse.DirectBalanceInquiryEnabled)
            {
                atse.DirectBalanceInquiryEnabled = false;
            }
        }

       
    }
}