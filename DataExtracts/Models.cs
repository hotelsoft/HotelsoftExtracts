using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExtracts
{
	public interface IRecord
	{
		Type GetRecordType();
	}

	#region Reservation Models
	public class ReservationRecord
	{
		/// <summary>
		/// Extraction date
		/// </summary>
		public DateTime ExtractDate { get; set; } = DateTime.Now;
		/// <summary>
		/// Reservation id
		/// </summary>
		public string ReservationId { get; set; }
		/// <summary>
		/// Creation date
		/// </summary>
		public DateTime CreationDate { get; set; }
		/// <summary>
		/// Checkin date
		/// </summary>
		public DateTime Checkin { get; set; }

		/// <summary>
		/// Checkout date
		/// </summary>
		public DateTime Checkout { get; set; }
		/// <summary>
		/// Room rate
		/// </summary>
		public double RoomRate { get; set; }
		/// <summary>
		/// End rate
		/// </summary>
		public double? EndRate { get; set; }
		public string RateCode { get; set; }
		public double? ChangeRate1a { get; set; }
		public double? ChangeRate2a { get; set; }
		public double? ChangeRate1b { get; set; }
		public double? ChangeRate2b { get; set; }
		public double? ChangeRate1c { get; set; }
		public double? ChangeRate2c { get; set; }
		public double? ChangeRate1d { get; set; }
		public double? ChangeRate2d { get; set; }
		public double? ChangeRate1e { get; set; }
		public double? ChangeRate2e { get; set; }
		public double? ChangeRate1f { get; set; }
		public double? ChangeRate2f { get; set; }
		public double? ChangeRate1g { get; set; }
		public double? ChangeRate2g { get; set; }
		public double? ChangeRate1h { get; set; }
		public double? ChangeRate2h { get; set; }
		public double? ChangeRate1i { get; set; }
		public double? ChangeRate2i { get; set; }
		public double? ChangeRate1j { get; set; }
		public double? ChangeRate2j { get; set; }
		public DateTime? ChangeDate1St { get; set; }
		public DateTime? ChangeDate1End { get; set; }
		public DateTime? ChangeDate2St { get; set; }
		public DateTime? ChangeDate2End { get; set; }
		public DateTime? ChangeDate3St { get; set; }
		public DateTime? ChangeDate3End { get; set; }
		public DateTime? ChangeDate4St { get; set; }
		public DateTime? ChangeDate4End { get; set; }
		public DateTime? ChangeDate5St { get; set; }
		public DateTime? ChangeDate5End { get; set; }
		public DateTime? ChangeDate6St { get; set; }
		public DateTime? ChangeDate6End { get; set; }
		public DateTime? ChangeDate7St { get; set; }
		public DateTime? ChangeDate7End { get; set; }
		public DateTime? ChangeDate8St { get; set; }
		public DateTime? ChangeDate8End { get; set; }
		public DateTime? ChangeDate9St { get; set; }
		public DateTime? ChangeDate9End { get; set; }
		public DateTime? ChangeDate10St { get; set; }
		public DateTime? ChangeDate10End { get; set; }
		/// <summary>
		/// Reservation status
		/// </summary>
		public string Status { get; set; }
		/// <summary>
		/// No.of adults
		/// </summary>
		public int Adults { get; set; }
		/// <summary>
		/// No.of children
		/// </summary>
		public int Children { get; set; }
		/// <summary>
		/// No.of Nights
		/// </summary>
		public int Nights { get; set; }
		/// <summary>
		/// Room Type
		/// </summary>
		public string RoomType { get; set; }
		/// <summary>
		/// No.of rooms
		/// </summary>
		public int Rooms { get; set; }
		/// <summary>
		/// Market segment code
		/// </summary>
		public string MarketCode { get; set; }
		/// <summary>
		/// Marketsegment Name
		/// </summary>
		public string MarketName { get; set; }
		/// <summary>
		/// Group code
		/// </summary>
		public string GroupCode { get; set; }
		/// <summary>
		/// Group Name
		/// </summary>
		public string GroupName { get; set; }
		/// <summary>
		/// Source code
		/// </summary>
		public string SourceCode { get; set; }
		/// <summary>
		/// Source name
		/// </summary>
		public string SourceName { get; set; }
		/// <summary>
		/// CompanyCode
		/// </summary>
		public string CompanyCode { get; set; }
		/// <summary>
		/// Company Name
		/// </summary>
		public string CompanyName { get; set; }
		/// <summary>
		/// Travel Agent code
		/// </summary>
		public string AgentCode { get; set; }
		/// <summary>
		/// Travel Agnet name
		/// </summary>
		public string AgentName { get; set; }
		/// <summary>
		/// Guest profile id
		/// </summary>
		public string GuestId { get; set; }
		/// <summary>
		/// Guest prefix
		/// </summary>
		public string Prefix { get; set; }
		/// <summary>
		/// Guest First Name
		/// </summary>
		public string FirstName { get; set; }
		/// <summary>
		/// Guest Last name
		/// </summary>
		public string LastName { get; set; }
		/// <summary>
		/// Guest Middle name
		/// </summary>
		public string MiddleName { get; set; }
		/// <summary>
		/// Guest Email
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// Guest phone number1
		/// </summary>
		public string Phone1 { get; set; }
		/// <summary>
		/// Guest phone number2
		/// </summary>
		public string Phone2 { get; set; }
		/// <summary>
		/// Guest street address
		/// </summary>
		public string Street1 { get; set; }
		/// <summary>
		/// Guest street2 address
		/// </summary>
		public string Street2 { get; set; }
		/// <summary>
		/// Guest address city
		/// </summary>
		public string City { get; set; }
		/// <summary>
		/// Guest address state
		/// </summary>
		public string State { get; set; }
		/// <summary>
		/// Guest address zip code
		/// </summary>
		public string ZipCode { get; set; }
		/// <summary>
		/// Guest address country
		/// </summary>
		public string Country { get; set; }
		/// <summary>
		/// Reservation comments
		/// </summary>
		public string Notes { get; set; }
	}

	public sealed class ReservationRecordMap : CsvClassMap<ReservationRecord>, IRecord
	{
		public ReservationRecordMap()
		{
			int index = 0;
			Map(m => m.ExtractDate).Name("EXTRACTDATE").Index(index++).TypeConverterOption("o");
			Map(m => m.ReservationId).Name("RESERVATIONID").Index(index++);
			Map(m => m.CreationDate).Name("CREATIONDT").Index(index++).TypeConverterOption("o");
			Map(m => m.Checkin).Name("CHECKIN").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.Checkout).Name("CHECKOUT").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.RateCode).Name("RATE_REQ").Index(index++);
			Map(m => m.RoomRate).Name("ROOMRATE").Index(index++);
			Map(m => m.EndRate).Name("ENDRATE").Index(index++);
			Map(m => m.ChangeRate1a).Name("CHANGERATE1A").Index(index++);
			Map(m => m.ChangeRate2a).Name("CHANGERATE2A").Index(index++);
			Map(m => m.ChangeDate1St).Name("CHANGEDATESA").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeDate1End).Name("CHANGEDATEEA").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeRate1b).Name("CHANGERATE1B").Index(index++);
			Map(m => m.ChangeRate2b).Name("CHANGERATE2B").Index(index++);
			Map(m => m.ChangeDate2St).Name("CHANGEDATESB").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeDate2End).Name("CHANGEDATEEB").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeRate1c).Name("CHANGERATE1C").Index(index++);
			Map(m => m.ChangeRate2c).Name("CHANGERATE2C").Index(index++);
			Map(m => m.ChangeDate3St).Name("CHANGEDATESC").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeDate3End).Name("CHANGEDATEEC").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeRate1d).Name("CHANGERATE1D").Index(index++);
			Map(m => m.ChangeRate2d).Name("CHANGERATE2D").Index(index++);
			Map(m => m.ChangeDate4St).Name("CHANGEDATESD").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeDate4End).Name("CHANGEDATEED").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeRate1e).Name("CHANGERATE1E").Index(index++);
			Map(m => m.ChangeRate2e).Name("CHANGERATE2E").Index(index++);
			Map(m => m.ChangeDate5St).Name("CHANGEDATESE").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeDate5End).Name("CHANGEDATEEE").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeRate1f).Name("CHANGERATE1F").Index(index++);
			Map(m => m.ChangeRate2f).Name("CHANGERATE2F").Index(index++);
			Map(m => m.ChangeDate6St).Name("CHANGEDATESF").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeDate6End).Name("CHANGEDATEEF").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeRate1g).Name("CHANGERATE1G").Index(index++);
			Map(m => m.ChangeRate2g).Name("CHANGERATE2G").Index(index++);
			Map(m => m.ChangeDate7St).Name("CHANGEDATESG").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeDate7End).Name("CHANGEDATEEG").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeRate1h).Name("CHANGERATE1H").Index(index++);
			Map(m => m.ChangeRate2h).Name("CHANGERATE2H").Index(index++);
			Map(m => m.ChangeDate8St).Name("CHANGEDATESH").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeDate8End).Name("CHANGEDATEEH").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeRate1i).Name("CHANGERATE1I").Index(index++);
			Map(m => m.ChangeRate2i).Name("CHANGERATE2I").Index(index++);
			Map(m => m.ChangeDate9St).Name("CHANGEDATESI").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeDate9End).Name("CHANGEDATEEI").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeRate1j).Name("CHANGERATE1J").Index(index++);
			Map(m => m.ChangeRate2j).Name("CHANGERATE2J").Index(index++);
			Map(m => m.ChangeDate10St).Name("CHANGEDATESJ").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.ChangeDate10End).Name("CHANGEDATEEJ").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.Status).Name("RESERVESTATUS").Index(index++);
			Map(m => m.Adults).Name("ADULTS").Index(index++);
			Map(m => m.Children).Name("CHILD").Index(index++);
			Map(m => m.Nights).Name("NIGHTS").Index(index++);
			Map(m => m.RoomType).Name("ROOMTYPE").Index(index++);
			Map(m => m.Rooms).Name("ROOMS").Index(index++);
			Map(m => m.MarketCode).Name("MARKETCD").Index(index++);
			Map(m => m.MarketName).Name("MARKETNAME").Index(index++);
			Map(m => m.GroupCode).Name("BLOCKID").Index(index++);
			Map(m => m.GroupName).Name("GROUPNAME").Index(index++);
			Map(m => m.SourceCode).Name("SOURCE").Index(index++);
			Map(m => m.SourceName).Name("SOURCENAME").Index(index++);
			Map(m => m.CompanyCode).Name("COMPANY").Index(index++);
			Map(m => m.CompanyName).Name("COMPANYNAME").Index(index++);
			Map(m => m.AgentCode).Name("TRAVELID").Index(index++);
			Map(m => m.AgentName).Name("TRAVELNAME").Index(index++);
			Map(m => m.GuestId).Name("GUESTID").Index(index++);
			Map(m => m.Prefix).Name("PREFIX").Index(index++);
			Map(m => m.FirstName).Name("FIRSTNAME").Index(index++);
			Map(m => m.LastName).Name("LASTNAME").Index(index++);
			Map(m => m.MiddleName).Name("MIDDLENAME").Index(index++);
			Map(m => m.Email).Name("EMAIL").Index(index++);
			Map(m => m.Phone1).Name("PHONE1").Index(index++);
			Map(m => m.Phone2).Name("PHONE2").Index(index++);
			Map(m => m.Street1).Name("STREET").Index(index++);
			Map(m => m.Street2).Name("STREET2").Index(index++);
			Map(m => m.City).Name("CITY").Index(index++);
			Map(m => m.State).Name("STATE").Index(index++);
			Map(m => m.ZipCode).Name("ZIPCODE").Index(index++);
			Map(m => m.Country).Name("COUNTRY").Index(index++);
			Map(m => m.Notes).Name("NOTES").Index(index++);
		}

		public Type GetRecordType()
		{
			return typeof(ReservationRecord);
		}
	}
	#endregion

	#region History Reservation Models
	public class HistReservationRecord
	{
		public DateTime ExtractDate { get; set; }
		public string ReservationId { get; set; }
		public string ConfirmationNum { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime Checkin { get; set; }
		public DateTime Checkout { get; set; }
		public string Status { get; set; }
		public int Adults { get; set; }
		public int Children { get; set; }
		public int Nights { get; set; }
		public string RoomType { get; set; }
		public string RoomNumber { get; set; }
		public string RateCode { get; set; }
		public string MarketCode { get; set; }
		public string MarketName { get; set; }
		public string GroupCode { get; set; }
		public string GroupName { get; set; }
		public string SourceCode { get; set; }
		public string SourceName { get; set; }
		public string CompanyCode { get; set; }
		public string CompanyName { get; set; }
		public string AgentCode { get; set; }
		public string AgentName { get; set; }
		public string GuestId { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string Email { get; set; }
		public string Phone1 { get; set; }
		public string Phone2 { get; set; }
		public string Street1 { get; set; }
		public string Street2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string Country { get; set; }
		public string Rates { get; set; }
		public string Notes { get; set; }
	}

	public sealed class HistReservationRecordMap : CsvClassMap<HistReservationRecord>, IRecord
	{
		public HistReservationRecordMap()
		{
			int index = 0;
			Map(m => m.ExtractDate).Name("EXTRACTDATE").Index(index++).TypeConverterOption("o");
			Map(m => m.ReservationId).Name("RESERVATIONID").Index(index++);
			Map(m => m.ConfirmationNum).Name("CONFNUM").Index(index++);
			Map(m => m.CreationDate).Name("CREATIONDT").Index(index++).TypeConverterOption("o");
			Map(m => m.Checkin).Name("CHECKIN").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.Checkout).Name("CHECKOUT").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.Adults).Name("ADULTS").Index(index++);
			Map(m => m.Children).Name("CHILD").Index(index++);
			Map(m => m.Nights).Name("NIGHTS").Index(index++);
			Map(m => m.RateCode).Name("RATE_REQ").Index(index++);
			Map(m => m.RoomNumber).Name("ROOMNUMBER").Index(index++);
			Map(m => m.RoomType).Name("ROOMTYPE").Index(index++);
			Map(m => m.MarketCode).Name("MARKETCD").Index(index++);
			Map(m => m.MarketName).Name("MARKETNAME").Index(index++);
			Map(m => m.GroupCode).Name("BLOCKID").Index(index++);
			Map(m => m.GroupName).Name("GROUPNAME").Index(index++);
			Map(m => m.SourceCode).Name("SOURCECD").Index(index++);
			Map(m => m.SourceName).Name("SOURCENAME").Index(index++);
			Map(m => m.CompanyCode).Name("COMPANY").Index(index++);
			Map(m => m.CompanyName).Name("COMPANYNAME").Index(index++);
			Map(m => m.AgentCode).Name("TRAVELID").Index(index++);
			Map(m => m.AgentName).Name("TRAVELNAME").Index(index++);
			Map(m => m.GuestId).Name("GUESTID").Index(index++);
			Map(m => m.Prefix).Name("PREFIX").Index(index++);
			Map(m => m.FirstName).Name("FIRSTNAME").Index(index++);
			Map(m => m.LastName).Name("LASTNAME").Index(index++);
			Map(m => m.MiddleName).Name("MIDDLENAME").Index(index++);
			Map(m => m.Email).Name("EMAIL").Index(index++);
			Map(m => m.Phone1).Name("PHONE1").Index(index++);
			Map(m => m.Phone2).Name("PHONE2").Index(index++);
			Map(m => m.Street1).Name("STREET").Index(index++);
			Map(m => m.Street2).Name("STREET2").Index(index++);
			Map(m => m.City).Name("CITY").Index(index++);
			Map(m => m.State).Name("STATE").Index(index++);
			Map(m => m.ZipCode).Name("ZIPCODE").Index(index++);
			Map(m => m.Country).Name("COUNTRY").Index(index++);
			Map(m => m.Rates).Name("RATES").Index(index++);
			Map(m => m.Notes).Name("NOTES").Index(index++);

		}
		public Type GetRecordType()
		{
			return typeof(HistReservationRecord);
		}
	}
	#endregion

	#region Availability Models
	public class AvailabilityRecord
	{
		public DateTime ExtractDate { get; set; }
		public DateTime StayDate { get; set; }
		public string RoomType { get; set; }
		public int Rooms { get; set; }
		public int StayOvers { get; set; }
		public int CheckIns { get; set; }
		public int OOOS { get; set; }
		public int Blocked { get; set; }
		public int Comps { get; set; }
		public int Owners { get; set; }
	}

	public sealed class AvailabilityRecordMap : CsvClassMap<AvailabilityRecord>, IRecord
	{
		public AvailabilityRecordMap()
		{
			int index = 0;
			Map(m => m.ExtractDate).Name("EXTRACTDATE").Index(index++).TypeConverterOption("o");
			Map(m => m.StayDate).Name("FOREDATE").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.RoomType).Name("ROOMTYPE").Index(index++);
			Map(m => m.Rooms).Name("ROOMS").Index(index++);
			Map(m => m.StayOvers).Name("STAYOVERS").Index(index++);
			Map(m => m.CheckIns).Name("CHECKINS").Index(index++);
			Map(m => m.OOOS).Name("OOOS").Index(index++);
			Map(m => m.Blocked).Name("BLOCKED").Index(index++);
			Map(m => m.Comps).Name("COMPLIMENTARY").Index(index++);
			Map(m => m.Owners).Name("OWNERRELATED").Index(index++);
		}

		public Type GetRecordType()
		{
			return typeof(AvailabilityRecord);
		}
	}
	#endregion

	#region Groups Models
	public class GroupsRecord
	{
		public DateTime ExtractDate { get; set; }
		public string Code { get; set; }
		public string Nane { get; set; }
		public string Status { get; set; }
		public DateTime CreateDate { get; set; }
		public DateTime Checkin { get; set; }
		public DateTime Checkout { get; set; }
		public DateTime? Cutoff { get; set; }
		public string RateCode { get; set; }
		public string MarketCode { get; set; }
		public string MarketName { get; set; }
		public string SourceCode { get; set; }
		public string SourceName { get; set; }
		public string CompanyCode { get; set; }
		public string CompanyName { get; set; }
		public string Rates { get; set; }
	}

	public sealed class GroupsRecordMap : CsvClassMap<GroupsRecord>, IRecord
	{
		public GroupsRecordMap()
		{
			int index = 0;
			Map(m => m.ExtractDate).Name("EXTRACTDATE").Index(index++).TypeConverterOption("o");
			Map(m => m.Code).Name("GROUPCODE").Index(index++);
			Map(m => m.Nane).Name("GROUPNAME").Index(index++);
			Map(m => m.Status).Name("GROUPSTATUS").Index(index++);
			Map(m => m.CreateDate).Name("CREATEDATE").Index(index++).TypeConverterOption("o");
			Map(m => m.Checkin).Name("CHECKIN").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.Checkout).Name("CHECKOUT").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.Cutoff).Name("CUTOFFDATE").Index(index++).TypeConverterOption("yyyy-MM-dd");
			Map(m => m.RateCode).Name("RATECODE").Index(index++);
			Map(m => m.MarketCode).Name("MARKETCD").Index(index++);
			Map(m => m.MarketName).Name("MARKETNAME").Index(index++);
			Map(m => m.SourceCode).Name("SOURCE").Index(index++);
			Map(m => m.SourceName).Name("SOURCENAME").Index(index++);
			Map(m => m.CompanyCode).Name("COMPANY").Index(index++);
			Map(m => m.Rates).Name("RATES").Index(index++);
		}

		public Type GetRecordType()
		{
			return typeof(GroupsRecord);
		}
	}
	#endregion
}
