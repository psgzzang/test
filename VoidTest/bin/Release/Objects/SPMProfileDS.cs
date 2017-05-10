using Gurock.SmartInspect;
using SPM.Configuration;
using SPM.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SPM.Objects
{
    public partial class SPMProfileDS
    {
        partial class CountryLoyaltyProcessorUrlsDataTable
        {
        }

        partial class CardProcessorUrlsDataTable
        {
        }

        partial class LoyaltyProcessorUrlDataTable
        {
            /// <summary>
            /// Returns the Custom loyalty endpoint row.
            /// </summary>
            /// <returns>Returns the Custom loyalty endpoint.</returns>
            public LoyaltyProcessorUrlRow GetCustomLoyaltyUrlRow()
            {
                return this.FindByLoyaltyProcessorUrlID((short)LoyaltyProcessorIDs.CustomLoyalty);
            }

            /// <summary>
            /// Returns the Unknown loyalty endpoint row for the environment.
            /// </summary>
            /// <returns>The production "Unknown" endpoint, if in production mode, or the test "Unknown" endpoint if in test mode.</returns>
            public LoyaltyProcessorUrlRow GetUnknownLoyaltyUrlRow()
            {

                return (SPMProfiles.ActiveProfile.IsProduction ? this.FindByLoyaltyProcessorUrlID((short)LoyaltyProcessorIDs.UnknownProduction)
                                                               : this.FindByLoyaltyProcessorUrlID((short)LoyaltyProcessorIDs.UnknownTest));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="slot"></param>
        public delegate void DisplayLogoDelegate(string filename, byte slot);

        #region Active Profile

        public partial class ActiveProfileDataTable
        {
        }

        public partial class ActiveProfileRow : INotifyPropertyChanged
        {
            /// <summary>
            ///
            /// </summary>
            /// <param name="displayMessage"></param>
            public void DisplayLogo(DisplayLogoDelegate displayMessage)
            {
                //Lookup the Store Brand record
                var ds = (SPMProfileDS)Table.DataSet;
                var storeBrandRow = ds.StoreBrand.FindByStoreBrandID(StoreBrandID);
                if (BrandLogoIsSaved)
                {
                    displayMessage(String.Empty, (byte)storeBrandRow.ImageSlot);
                }
                else
                {
                    BrandLogoIsSaved = true;
                    displayMessage(storeBrandRow.ImageFilename, (byte)storeBrandRow.ImageSlot);
                }
            }

            /// <summary>
            /// Returns the current StoreBrand
            /// </summary>
            public StoreBrandRow StoreBrand
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.StoreBrand.FindByStoreBrandID(StoreBrandID);
                }
                set
                {
                    if (StoreBrandID != value.StoreBrandID)
                    {
                        BrandLogoIsSaved = false;
                        StoreBrandID = value.StoreBrandID;
                        Continent = value.Continents().First();
                        OnPropertyChanged(new PropertyChangedEventArgs("StoreBrandID"));
                        OnPropertyChanged(new PropertyChangedEventArgs("BrandLogoIsSaved"));
                    }
                }
            }

            /// <summary>
            /// Returns the current Continent
            /// </summary>
            public ContinentRow Continent
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.Continent.FindByContinentID(ContinentID);
                }
                set
                {
                    if (ContinentID != value.ContinentID)
                    {
                        ContinentID = value.ContinentID;
                        Country = Continent.Countries().First();
                        OnPropertyChanged(new PropertyChangedEventArgs("ContinentID"));
                    }
                }
            }

            /// <summary>
            /// Returns the current Country
            /// </summary>
            public CountryRow Country
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.Country.FindByCountryID(CountryID);
                }
                set
                {
                    try
                    {
                        if (CountryID != value.CountryID)
                        {
                            var ds = (SPMProfileDS)Table.DataSet;
                            CountryID = value.CountryID;
                            Language = Country.Languages().First();
                            CardProcessorUrl = Country.CardProcessorUrls(IsProduction).First();
                            CardProcessorPhone = Country.CardProcessorPhones(IsProduction).First();
                            LoyaltyProcessorURL = Country.GetLoyaltyProcessorURLs(IsProduction).First();
                            PaymentManagerSettings.Instance.SnapLogicGatewayEncryptedBearerToken = PaymentManagerSettings.Instance.GetDefaultSnapLogicBearerTokens(LoyaltyProcessorURL);
                            // the 'Active CardType Devices' need to be updated as well
                            switch (value.Code)
                            {
                                case "USA":
                                    DefaultSPMProfiles.SetUSDefaultActiveCardTypeDevice(ds);
                                    break;

                                case "CAN":
                                    DefaultSPMProfiles.SetCANDefaultActiveCardTypeDevice(ds);
                                    break;

                                case "DEU":
                                    DefaultSPMProfiles.SetDEUDefaultActiveCardTypeDevice(ds);
                                    break;

                                case "GBR":
                                    DefaultSPMProfiles.SetGBRDefaultActiveCardTypeDevice(ds);
                                    break;

                                case "AUS":
                                    DefaultSPMProfiles.SetAUSDefaultActiveCardTypeDevice(ds);
                                    break;
                                case "AUT":
                                    DefaultSPMProfiles.SetAUTDefaultActiveCardTypeDevice(ds);
                                    break;

                                default:
                                    DefaultSPMProfiles.SetDefaultActiveCardTypeDevice(ds);
                                    break;
                            }
                            OnPropertyChanged(new PropertyChangedEventArgs("CountryID"));
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.Log.LogException("Error setting the Country", ex);
                    }
                }
            }

            /// <summary>
            /// Returns the CultureInfo object associated with the ActiveProfile Language and Country properties.
            /// Returns null if "[language]-[country]" and "[language]" are both invalid. 
            /// </summary>
            public CultureInfo ProfileCulture
            {
                get
                {
                    return CultureHelper.GetCultureInfo();
                }
            }

            /// <summary>
            /// Returns the current Language
            /// </summary>
            public LanguageRow Language
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.Language.FindByLanguageID(LanguageID);
                }
                set
                {
                    if (LanguageID != value.LanguageID)
                    {
                        LanguageID = value.LanguageID;
                        OnPropertyChanged(new PropertyChangedEventArgs("LanguageID"));
                    }
                }
            }

            /// <summary>
            /// Returns the current Card Processor URL
            /// </summary>
            public CardProcessorUrlRow CardProcessorUrl
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.CardProcessorUrl.FindByCardProcessorUrlID(CardProcessorUrlID);
                }
                set
                {
                    if (CardProcessorUrlID != value.CardProcessorUrlID)
                    {
                        CardProcessorUrlID = value.CardProcessorUrlID;
                        OnPropertyChanged(new PropertyChangedEventArgs("CardProcessorUrlID"));
                    }
                }
            }

            /// <summary>
            /// Returns the current Card processor phone
            /// </summary>
            public CardProcessorPhoneRow CardProcessorPhone
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.CardProcessorPhone.FindByCardProcessorPhoneID(CardProcessorPhoneID);
                }
                set
                {
                    if (CardProcessorPhoneID != value.CardProcessorPhoneID)
                    {
                        CardProcessorPhoneID = value.CardProcessorPhoneID;
                        OnPropertyChanged(new PropertyChangedEventArgs("CardProcessorPhoneID"));
                    }
                }
            }

            /// <summary>
            /// Returns the current Loyalty Processor URL
            /// </summary>
            public LoyaltyProcessorUrlRow LoyaltyProcessorURL
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.LoyaltyProcessorUrl.FindByLoyaltyProcessorUrlID(LoyaltyProcessorUrlID);
                }
                set
                {
                    if (LoyaltyProcessorUrlID != value.LoyaltyProcessorUrlID)
                    {
                        LoyaltyProcessorUrlID = value.LoyaltyProcessorUrlID;
                        OnPropertyChanged(new PropertyChangedEventArgs("LoyaltyProcessorUrlID"));
                    }
                }
            }

            /// <summary>
            /// The list of acceptable card types for the chosen country
            /// </summary>
            public IEnumerable<CardTypeRow> ValidCardTypes
            {
                get
                {
                    return Country.ValidCardTypes;
                }
            }

            /// <summary>
            /// The list of URLs that are valid for the current country
            /// </summary>
            /// <returns></returns>
            public IEnumerable<CardProcessorUrlRow> ValidURLs()
            {
                return Country.CardProcessorUrls(IsProduction);
            }

            /// <summary>
            /// The list of Phone numbers that are valid for the current country
            /// </summary>
            /// <returns></returns>
            public IEnumerable<CardProcessorPhoneRow> ValidPhones()
            {
                return Country.CardProcessorPhones(IsProduction);
            }

            /// <summary>
            /// Returns an enumerable collection of valid loyalty URLs based on the active profile's current country.
            /// </summary>
            /// <returns></returns>
            public IEnumerable<LoyaltyProcessorUrlRow> GetLoyaltyURLs()
            {
                return Country.GetLoyaltyProcessorURLs(IsProduction);
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="cardTypeID"></param>
            /// <returns></returns>
            public IEnumerable<CardReaderDeviceRow> ValidCardReaderDevicesPerCardType(CardTypeConstants cardTypeID)
            {
                return Country.CardReaderDevicesPerCardType(cardTypeID);
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="cardTypeID"></param>
            /// <returns></returns>
            public bool CanEditCardType(CardTypeConstants cardTypeID)
            {
                return Country.CanEditCardType(cardTypeID);
            }

            /// <summary>
            ///
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            ///
            /// </summary>
            /// <param name="e"></param>
            private void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        #endregion Active Profile

        #region Active Card Type Device

        public partial class ActiveCardTypeDeviceDataTable
        {
            /// <summary>
            ///
            /// </summary>
            public List<CardTypeRow> ActiveCardTypes
            {
                get { return this.Select(row => row.CardType).Distinct().ToList(); }
            }

            /// <summary>
            ///
            /// </summary>
            public List<CardReaderDeviceRow> ActiveDevices
            {
                get { return this.Select(row => row.CardReaderDevice).Distinct().ToList(); }
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="cardTypeID"></param>
            /// <returns></returns>
            public List<CardReaderDeviceRow> ActiveDevicesPerCardType(CardTypeConstants cardTypeID)
            {
                List<CardReaderDeviceRow> rtn = null;
                try
                {
                    rtn = (from row in this
                           where row.CardTypeID == cardTypeID
                           select row.CardReaderDevice).ToList();
                }
                catch (Exception ex)
                {
                    Trace.Log.LogException("Error in ActiveDevicesPerCardType", ex);
                }
                return rtn;
            }

            /// <summary>
            ///
            /// </summary>
            public List<DeviceTypes> ActiveDeviceTypes
            {
                get { return this.Select(row => row.CardReaderDeviceID).Distinct().ToList(); }
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public string ToJSON()
            {
                var rtn = new StringBuilder();
                rtn.Append(@"{""List"":[");
                foreach (ActiveCardTypeDeviceRow row in this)
                {
                    rtn.AppendFormat(@"{{""ct"":""{0}"",""dev"":""{1}""}},",
                                     row.CardType.Code,
                                     row.CardReaderDevice.Name);
                }
                rtn.Append(@"]}");
                return rtn.ToString();
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="cardTypeID"></param>
            /// <returns></returns>
            public bool CheckForCardSecurity(CardTypeConstants cardTypeID)
            {
                return (from row in this
                        where row.CardTypeID == cardTypeID
                        select row.CardType).First().CheckSecureCard;
            }
        }

        public partial class ActiveCardTypeDeviceRow : INotifyPropertyChanged
        {
            /// <summary>
            ///
            /// </summary>
            public CardTypeRow CardType
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.CardType.FindByCardTypeID(CardTypeID);
                }
            }

            /// <summary>
            ///
            /// </summary>
            public CardReaderDeviceRow CardReaderDevice
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.CardReaderDevice.FindByCardReaderDeviceID(CardReaderDeviceID);
                }
            }

            /// <summary>
            ///
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        #endregion Active Card Type Device

        #region Card Type

        public partial class CardTypeDataTable
        {
        }

        #endregion Card Type

        #region Card Processing URL

        public partial class CardProcessorUrlDataTable
        {
            public CardProcessorUrlRow GetCustomCardProcessorUrlRow()
            {
                return this.FindByCardProcessorUrlID((short)CardProcessorIDs.CustomCardProcessor);
            }

            public CardProcessorUrlRow GetUnknownCardProcessorUrlRow()
            {
                return (SPMProfiles.ActiveProfile.IsProduction ? this.FindByCardProcessorUrlID((short)CardProcessorIDs.UnknownProduction)
                                                               : this.FindByCardProcessorUrlID((short)CardProcessorIDs.UnknownTest));
            }
        }

        public partial class CardProcessorUrlRow
        {
        }

        #endregion Card Processing URL

        #region Continent

        public partial class ContinentDataTable
        {
        }

        public partial class ContinentRow
        {
            /// <summary>
            /// Returns all countries for this continent
            /// </summary>
            /// <returns></returns>
            public IEnumerable<CountryRow> Countries()
            {
                var ds = (SPMProfileDS)Table.DataSet;
                return from cty in ds.Country
                       where cty.ContinentID == ContinentID
                       select cty;
            }
        }

        #endregion Continent

        #region Country

        public partial class CountryDataTable
        {
            /// <summary>
            ///
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public CountryRow FindByCode(string code)
            {
                return this.FirstOrDefault(row => row.Code.ToUpper().Equals(code.ToUpper()));
            }

            /// <summary>
            /// Adds Urls by IDs to all countries
            /// </summary>
            /// <param name="urlID">Url ID to add to countries</param>
            public void AddCountryCardProcessorUrlsRows(short urlID)
            {
                if (urlID > 0)
                {
                    try
                    {
                        //Check to see that the urlID is a valid value, raise an exception if not valid
                        if (SPMProfiles.Data.CardProcessorUrl.FindByCardProcessorUrlID(urlID) != null)
                        {
                            SPMProfileDS.CountryCardProcessorUrlsDataTable dt = SPMProfiles.Data.CountryCardProcessorUrls;
                            foreach (SPMProfileDS.CountryRow row in this.Rows)
                            {
                                dt.AddCountryCardProcessorUrlsRow(row.CountryID, urlID);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.Log.LogException("Invalid URL id", ex);
                    }
                }
            }
        }

        public partial class CountryRow
        {
            /// <summary>
            /// Returns the Store Brands for which this country can select
            /// </summary>
            /// <returns></returns>
            public IEnumerable<StoreBrandRow> StoreBrands()
            {
                var ds = (SPMProfileDS)Table.DataSet;
                return from sb in ds.StoreBrand.AsEnumerable()
                       join sbc in ds.StoreBrandCountry.AsEnumerable()
                           on sb.StoreBrandID equals sbc.StoreBrandID
                       where sbc.CountryID == CountryID
                       select sb;
            }

            /// <summary>
            /// Returns languages for this Country
            /// </summary>
            /// <returns></returns>
            public IEnumerable<LanguageRow> Languages()
            {
                var ds = (SPMProfileDS)Table.DataSet;
                return from l in ds.Language.AsEnumerable()
                       join cl in ds.CountryLanguage.AsEnumerable()
                           on l.LanguageID equals cl.LanguageID
                       where cl.CountryID == CountryID
                       select l;
            }

            /// <summary>
            /// returns all of the Card processing Urls
            /// </summary>
            /// <returns></returns>
            public IEnumerable<CardProcessorUrlRow> CardProcessorUrls(bool isProduction)
            {
                var ds = (SPMProfileDS)Table.DataSet;
                IEnumerable<CardProcessorUrlRow> query;
#if SDK
                query = from cpu in ds.CardProcessorUrl.AsEnumerable()
                        join ccpu in ds.CountryCardProcessorUrls.AsEnumerable()
                            on cpu.CardProcessorUrlID equals ccpu.CardProcessorUrlID
                        where ccpu.CountryID == CountryID && cpu.IsProduction == isProduction
                        select cpu;
#else
                query = from cpu in ds.CardProcessorUrl.AsEnumerable()
                        join ccpu in ds.CountryCardProcessorUrls.AsEnumerable()
                            on cpu.CardProcessorUrlID equals ccpu.CardProcessorUrlID
                        where ccpu.CountryID == CountryID && cpu.IsProduction == isProduction && cpu.CardProcessorUrlID != (short)CardProcessorIDs.CustomCardProcessor
                        select cpu;
#endif

                return query;
            }

            /// <summary>
            /// returns all of the Card processing Urls
            /// </summary>
            /// <returns></returns>
            public IEnumerable<CardProcessorPhoneRow> CardProcessorPhones(bool isProduction)
            {
                var ds = (SPMProfileDS)Table.DataSet;
                return from cp in ds.CardProcessorPhone.AsEnumerable()
                       join cu in ds.CountryPhone.AsEnumerable()
                           on cp.CardProcessorPhoneID equals cu.CardProcessingPhoneID
                       where cu.CountryID == CountryID && cp.IsProduction == isProduction
                       select cp;
            }

            public IEnumerable<LoyaltyProcessorUrlRow> GetLoyaltyProcessorURLs(bool isProduction)
            {
                SPMProfileDS ds = (SPMProfileDS)Table.DataSet;
                IEnumerable<LoyaltyProcessorUrlRow> query;
                // Currently mimics CardProcessorURL logic. Will not differentiate loyalty processor URLs depending on region.
#if SDK
                query = from clpu in ds.CountryLoyaltyProcessorUrls.AsEnumerable()
                        join lpu in ds.LoyaltyProcessorUrl.AsEnumerable()
                            on clpu.LoyaltyProcessorUrlID equals lpu.LoyaltyProcessorUrlID
                        where clpu.CountryID == CountryID && lpu.IsProduction == isProduction
                        select lpu;
#else
                query = from clpu in ds.CountryLoyaltyProcessorUrls.AsEnumerable()
                        join lpu in ds.LoyaltyProcessorUrl.AsEnumerable()
                            on clpu.LoyaltyProcessorUrlID equals lpu.LoyaltyProcessorUrlID
                        where clpu.CountryID == CountryID && lpu.IsProduction == isProduction && lpu.LoyaltyProcessorUrlID != (short)LoyaltyProcessorIDs.CustomLoyalty
                        select lpu;
#endif
                return query;
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public IEnumerable<CardProcessorUrlRow> ProductionURLs()
            {
                return CardProcessorUrls(true);
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public IEnumerable<CardProcessorUrlRow> TestingURLs()
            {
                return CardProcessorUrls(false);
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public IEnumerable<CardProcessorPhoneRow> ProductionPhones()
            {
                return CardProcessorPhones(true);
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public IEnumerable<CardProcessorPhoneRow> TestingPhones()
            {
                return CardProcessorPhones(false);
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="cardTypeID"></param>
            /// <returns></returns>
            public IEnumerable<CardReaderDeviceRow> CardReaderDevicesPerCardType(CardTypeConstants cardTypeID)
            {
                var ds = (SPMProfileDS)Table.DataSet;
                return from crd in ds.CardReaderDevice.AsEnumerable()
                       join ct in ds.CountryCardTypeDevice.AsEnumerable()
                           on crd.CardReaderDeviceID equals ct.CardReaderDeviceID
                       where ct.CountryID == CountryID && ct.CardTypeID == cardTypeID
                       select crd;
            }

            /// <summary>
            ///
            /// </summary>
            public IEnumerable<CardTypeRow> ValidCardTypes
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return from ct in ds.CardType.AsEnumerable()
                           join cct in ds.CountryCardType.AsEnumerable()
                               on ct.CardTypeID equals cct.CardTypeID
                           where cct.CountryID == CountryID
                           select ct;
                }
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="cardTypeID"></param>
            /// <returns></returns>
            public bool CanEditCardType(CardTypeConstants cardTypeID)
            {
                var ds = (SPMProfileDS)Table.DataSet;
                CountryCardTypeRow row = ds.CountryCardType.FindByCountryIDCardTypeID(CountryID, cardTypeID);
                return row.CanEdit;
            }

            /// <summary>
            ///
            /// </summary>
            /// <returns></returns>
            public bool SupportsElectronicPayments()
            {
                return ValidCardTypes.Any();
            }
        }

        #endregion Country

        #region Country Card Type Device

        public partial class CountryCardTypeDeviceDataTable
        {
            /// <summary>
            ///
            /// </summary>
            /// <param name="countryID"></param>
            /// <param name="cardTypeID"></param>
            /// <returns></returns>
            public List<CardReaderDeviceRow> DevicesPerCardType(short countryID, CardTypeConstants cardTypeID)
            {
                return (from row in this
                        where row.CountryID == countryID &&
                              row.CardTypeID == cardTypeID
                        select row.CardReaderDevice).ToList();
            }
        }

        public partial class CountryCardTypeDeviceRow
        {
            /// <summary>
            ///
            /// </summary>
            public CardTypeRow CardType
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.CardType.FindByCardTypeID(CardTypeID);
                }
            }

            /// <summary>
            ///
            /// </summary>
            public CardReaderDeviceRow CardReaderDevice
            {
                get
                {
                    var ds = (SPMProfileDS)Table.DataSet;
                    return ds.CardReaderDevice.FindByCardReaderDeviceID(CardReaderDeviceID);
                }
            }
        }

        #endregion Country Card Type Device

        #region CardReaderDeviceRow

        public partial class CardReaderDeviceRow
        {
            ///<summary>
            ///</summary>
            public void LogMe()
            {
                LogMe("Card Reader Device Row");
            }

            ///<summary>
            ///</summary>
            ///<param name="title"></param>
            public void LogMe(string title)
            {
                try
                {
                    using (var ctx = new InspectorViewerContext())
                    {
                        ctx.StartGroup("Card Reader Info");
                        ctx.AppendKeyValue("Name", Name);
                        ctx.AppendKeyValue("CardReaderDeviceID", CardReaderDeviceID);
                        ctx.AppendKeyValue("SupportsManualEntry", SupportsManualEntry);
                        ctx.AppendKeyValue("TimeoutCardData", TimeoutCardData);
                        Trace.Log.LogCustomContext(title, ctx);
                    }
                }
                catch (Exception ex)
                {
                    Trace.Log.LogException("Error in LogMe for CardReaderDeviceRow", ex);
                }
            }
        }

        #endregion CardReaderDeviceRow

        #region Store Brand

        public partial class StoreBrandDataTable
        {
            /// <summary>
            ///
            /// </summary>
            public IEnumerable<StoreBrandRow> AllStoreBrands
            {
                get { return from sb in this.AsEnumerable() select sb; }
            }
        }

        public partial class StoreBrandRow
        {
            /// <summary>
            /// Returns all continents for this store brand
            /// </summary>
            /// <returns></returns>
            public IEnumerable<ContinentRow> Continents()
            {
                var ds = (SPMProfileDS)Table.DataSet;
                return from sbc in ds.StoreBrandContinent.AsEnumerable()
                       join cont in ds.Continent.AsEnumerable()
                           on sbc.ContinentID equals cont.ContinentID
                       where sbc.StoreBrandID == StoreBrandID
                       select cont;
            }

            /// <summary>
            /// Returns all countries for this store brand, with the chosen contenant
            /// </summary>
            /// <returns></returns>
            public IEnumerable<CountryRow> Countries(ContinentConstants activeContinent)
            {
                var ds = (SPMProfileDS)Table.DataSet;
                var rtn = from sbc in ds.StoreBrandCountry.AsEnumerable()
                          join cty in ds.Country.AsEnumerable()
                              on sbc.CountryID equals cty.CountryID
                          where sbc.StoreBrandID == StoreBrandID && cty.ContinentID == activeContinent
                          select cty;
                return rtn;
            }
        }

        #endregion Store Brand

        #region SmartPOS

        public partial class AllowSmartPOSDataTable
        {
            /// <summary>
            ///
            /// </summary>
            /// <param name="countryID"></param>
            /// <returns></returns>
            public bool Contains(Int16 countryID)
            {
                return this.Any(allowSmartPOSRow => allowSmartPOSRow.CountryID == countryID);
            }
        }

        #endregion SmartPOS

        #region Reporting

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal DataSetReport[] ToArray()
        {
            IEnumerable<DataSetReport> results = from cont in Continent.AsEnumerable()
                                                 join coun in Country.AsEnumerable()
                                                     on cont.ContinentID equals coun.ContinentID
                                                 join cl in CountryLanguage.AsEnumerable()
                                                     on coun.CountryID equals cl.CountryID
                                                 join lang in Language.AsEnumerable()
                                                     on cl.LanguageID equals lang.LanguageID
                                                 join cu in CountryCardProcessorUrls.AsEnumerable()
                                                     on coun.CountryID equals cu.CountryID
                                                 join cpu in CardProcessorUrl
                                                     on cu.CardProcessorUrlID equals cpu.CardProcessorUrlID
                                                 join cp in CountryPhone.AsEnumerable()
                                                     on coun.CountryID equals cp.CountryID
                                                 join cpp in CardProcessorPhone.AsEnumerable()
                                                     on cp.CardProcessingPhoneID equals cpp.CardProcessorPhoneID
                                                 join cctd in CountryCardTypeDevice.AsEnumerable()
                                                     on coun.CountryID equals cctd.CountryID
                                                 join ct in CardType.AsEnumerable()
                                                     on cctd.CardTypeID equals ct.CardTypeID
                                                 join crd in CardReaderDevice.AsEnumerable()
                                                     on cctd.CardReaderDeviceID equals crd.CardReaderDeviceID
                                                 select new DataSetReport
                                                 {
                                                     ContinentName = cont.Name,
                                                     CountryName = coun.Name,
                                                     LanguageName = lang.Name,
                                                     URLName = cpu.Name,
                                                     URLValue = cpu.Value,
                                                     PhoneName = cpp.Name,
                                                     PhoneValue = cpp.Value,
                                                     CardTypeName = ct.Name,
                                                     DeviceName = crd.Name
                                                 };
            return results.ToArray();
        }

        internal class DataSetReport
        {
            internal string ContinentName { get; set; }
            internal string CountryName { get; set; }
            internal string LanguageName { get; set; }
            internal string URLName { get; set; }
            internal string URLValue { get; set; }
            internal string PhoneName { get; set; }
            internal string PhoneValue { get; set; }
            internal string CardTypeName { get; set; }
            internal string DeviceName { get; set; }
        }

        #endregion Reporting
    }
}