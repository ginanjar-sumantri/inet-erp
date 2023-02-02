using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.DataMapping
{
    public static class AppModule
    {
        public static string GetText(String _prmModule)
        {
            string _result;

            switch (_prmModule.Trim())
            {
                case "5":
                    _result = "Home";
                    break;
                case "1":
                    _result = "Accounting";
                    break;
                case "2":
                    _result = "Executive Information";
                    break;
                case "3":
                    _result = "Finance";
                    break;
                case "4":
                    _result = "Human Resource";
                    break;
                case "6":
                    _result = "Payroll";
                    break;
                case "7":
                    _result = "Production";
                    break;
                case "8":
                    _result = "Purchasing";
                    break;
                case "9":
                    _result = "Sales";
                    break;
                case "10":
                    _result = "Settings";
                    break;
                case "11":
                    _result = "Ship Porting";
                    break;
                case "12":
                    _result = "Stock Control";
                    break;
                case "13":
                    _result = "Billing";
                    break;
                case "14":
                    _result = "NCC";
                    break;
                case "15":
                    _result = "SMS";
                    break;
                case "16":
                    _result = "Clinic";
                    break;
                case "17":
                    _result = "POS";
                    break;
                case "19":
                    _result = "Tour";
                    break;
                default:
                    _result = "Home";
                    break;
            }

            return _result;
        }

        public static string GetText(ERPModule _prmModule)
        {
            string _result;

            switch (_prmModule)
            {
                case ERPModule.Home:
                    _result = "Home";
                    break;
                case ERPModule.Accounting:
                    _result = "Accounting";
                    break;
                case ERPModule.ExecutiveInformation:
                    _result = "Executive Information";
                    break;
                case ERPModule.Finance:
                    _result = "Finance";
                    break;
                case ERPModule.HumanResource:
                    _result = "Human Resource";
                    break;
                case ERPModule.Payroll:
                    _result = "Payroll";
                    break;
                case ERPModule.Production:
                    _result = "Production";
                    break;
                case ERPModule.Purchasing:
                    _result = "Purchasing";
                    break;
                case ERPModule.Sales:
                    _result = "Sales";
                    break;
                case ERPModule.Settings:
                    _result = "Settings";
                    break;
                case ERPModule.ShipPorting:
                    _result = "Ship Porting";
                    break;
                case ERPModule.StockControl:
                    _result = "Stock Control";
                    break;
                case ERPModule.Billing:
                    _result = "Billing";
                    break;
                case ERPModule.NCC:
                    _result = "NCC";
                    break;
                case ERPModule.SMS:
                    _result = "SMS";
                    break;
                case ERPModule.Clinic:
                    _result = "Clinic";
                    break;
                case ERPModule.POS:
                    _result = "POS";
                    break;
                case ERPModule.Tour:
                    _result = "Tour";
                    break;
                default:
                    _result = "Home";
                    break;
            }

            return _result;
        }

        public static string GetValue(ERPModule _prmModule)
        {
            string _result;

            switch (_prmModule)
            {
                case ERPModule.Home:
                    _result = "5";
                    break;
                case ERPModule.Accounting:
                    _result = "1";
                    break;
                case ERPModule.ExecutiveInformation:
                    _result = "2";
                    break;
                case ERPModule.Finance:
                    _result = "3";
                    break;
                case ERPModule.HumanResource:
                    _result = "4";
                    break;
                case ERPModule.Payroll:
                    _result = "6";
                    break;
                case ERPModule.Production:
                    _result = "7";
                    break;
                case ERPModule.Purchasing:
                    _result = "8";
                    break;
                case ERPModule.Sales:
                    _result = "9";
                    break;
                case ERPModule.Settings:
                    _result = "10";
                    break;
                case ERPModule.ShipPorting:
                    _result = "11";
                    break;
                case ERPModule.StockControl:
                    _result = "12";
                    break;
                case ERPModule.Billing:
                    _result = "13";
                    break;
                case ERPModule.NCC:
                    _result = "14";
                    break;
                case ERPModule.SMS:
                    _result = "15";
                    break;
                case ERPModule.Clinic:
                    _result = "16";
                    break;
                case ERPModule.POS:
                    _result = "17";
                    break;
                case ERPModule.Tour:
                    _result = "19";
                    break;
                default:
                    _result = "5";
                    break;
            }

            return _result;
        }

        public static ERPModule GetValue(string _prmValue)
        {
            ERPModule _result;

            switch (_prmValue.Trim())
            {
                case "5":
                    _result = ERPModule.Home;
                    break;
                case "1":
                    _result = ERPModule.Accounting;
                    break;
                case "2":
                    _result = ERPModule.ExecutiveInformation;
                    break;
                case "3":
                    _result = ERPModule.Finance;
                    break;
                case "4":
                    _result = ERPModule.HumanResource;
                    break;
                case "6":
                    _result = ERPModule.Payroll;
                    break;
                case "7":
                    _result = ERPModule.Production;
                    break;
                case "8":
                    _result = ERPModule.Purchasing;
                    break;
                case "9":
                    _result = ERPModule.Sales;
                    break;
                case "10":
                    _result = ERPModule.Settings;
                    break;
                case "11":
                    _result = ERPModule.ShipPorting;
                    break;
                case "12":
                    _result = ERPModule.StockControl;
                    break;
                case "13":
                    _result = ERPModule.Billing;
                    break;
                case "14":
                    _result = ERPModule.NCC;
                    break;
                case "15":
                    _result = ERPModule.SMS;
                    break;
                case "16":
                    _result = ERPModule.Clinic;
                    break;
                case "17":
                    _result = ERPModule.POS;
                    break;
                case "19":
                    _result = ERPModule.Tour;
                    break;
                default:
                    _result = ERPModule.Home;
                    break;
            }

            return _result;
        }

        public static string GetValue(TransactionType _prmTransType)
        {
            string _result = "";

            switch (_prmTransType)
            {
                case TransactionType.Transaction:
                    _result = "TRN";
                    break;
                case TransactionType.JournalEntry:
                    _result = "JE";
                    break;
                case TransactionType.PettyCash:
                    _result = "PCO";
                    break;
                case TransactionType.FABeginning:
                    _result = "FAB";
                    break;
                case TransactionType.FADevaluation:
                    _result = "FAO";
                    break;
                case TransactionType.FAMoving:
                    _result = "FAM";
                    break;
                case TransactionType.PaymentNonPurchase:
                    _result = "PYN";
                    break;
                case TransactionType.PaymentTrade:
                    _result = "PYT";
                    break;
                case TransactionType.FAProcess:
                    _result = "FAD";
                    break;
                case TransactionType.FASales:
                    _result = "FAJ";
                    break;
                case TransactionType.FATenancy:
                    _result = "FAT";
                    break;
                case TransactionType.ReceiptNonTrade:
                    _result = "ORN";
                    break;
                case TransactionType.NotaDebitSupplier:
                    _result = "DNS";
                    break;
                case TransactionType.NotaCreditSupplier:
                    _result = "CNS";
                    break;
                case TransactionType.NotaCreditCustomer:
                    _result = "CNC";
                    break;
                case TransactionType.NotaDebitCustomer:
                    _result = "DNC";
                    break;
                case TransactionType.ReceiptTrade:
                    _result = "ORT";
                    break;
                case TransactionType.ChangeGiroPayment:
                    _result = "CGO";
                    break;
                case TransactionType.FINDPCustList:
                    _result = "DPL";
                    break;
                case TransactionType.ChangeGiroReceipt:
                    _result = "CGI";
                    break;
                case TransactionType.DPCustomer:
                    _result = "DPC";
                    break;
                case TransactionType.FINDPCustRetur:
                    _result = "DCR";
                    break;
                case TransactionType.DPSuppPay:
                    _result = "DPS";
                    break;
                case TransactionType.FINDPSuppRetur:
                    _result = "DSR";
                    break;
                case TransactionType.APRate:
                    _result = "APR";
                    break;
                case TransactionType.ARRate:
                    _result = "ARR";
                    break;
                case TransactionType.StockIssueRequest:
                    _result = "SIR";
                    break;
                case TransactionType.StockIssueRequestFA:
                    _result = "SIF";
                    break;
                case TransactionType.StockTransRequest:
                    _result = "STR";
                    break;
                case TransactionType.FAPurchase:
                    _result = "FAP";
                    break;
                case TransactionType.RequestSalesRetur:
                    _result = "SRR";
                    break;
                case TransactionType.PurchaseRequest:
                    _result = "SRQ";
                    break;
                case TransactionType.StockAdjustment:
                    _result = "SAD";
                    break;
                case TransactionType.StockBeginning:
                    _result = "SSD";
                    break;
                case TransactionType.StockOpname:
                    _result = "SOP";
                    break;
                case TransactionType.StockIssueToFA:
                    _result = "SFA";
                    break;
                case TransactionType.StockReceivingRetur:
                    _result = "RRR";
                    break;
                case TransactionType.StockRejectOut:
                    _result = "SPO";
                    break;
                case TransactionType.StockRejectIn:
                    _result = "SPI";
                    break;
                case TransactionType.StockTransferExternalRR:
                    _result = "STI";
                    break;
                case TransactionType.StockTransferExternalSJ:
                    _result = "STO";
                    break;
                case TransactionType.StockReceivingOther:
                    _result = "SRO";
                    break;
                case TransactionType.StockReceivingCustomer:
                    _result = "SRC";
                    break;
                case TransactionType.StockReceivingSupplier:
                    _result = "SRS";
                    break;
                case TransactionType.StockTransferInternal:
                    _result = "STG";
                    break;
                case TransactionType.StockIssueSlip:
                    _result = "SWI";
                    break;
                case TransactionType.StockChangeGood:
                    _result = "SCH";
                    break;
                case TransactionType.SalesOrder:
                    _result = "SO";
                    break;
                case TransactionType.DeliveryOrder:
                    _result = "DO";
                    break;
                case TransactionType.BillOfLading:
                    _result = "SJ";
                    break;
                case TransactionType.PettyCashReceive:
                    _result = "PCR";
                    break;
                case TransactionType.PurchaseOrder:
                    _result = "PO";
                    break;
                case TransactionType.ReceivingPO:
                    _result = "RR";
                    break;
                case TransactionType.CustomerNote:
                    _result = "CI";
                    break;
                case TransactionType.SupplierNote:
                    _result = "SI";
                    break;
                case TransactionType.BillingInvoice:
                    _result = "INV";
                    break;
                case TransactionType.CustomerBillingAccount:
                    _result = "CBA";
                    break;
                case TransactionType.CustomerInvoice:
                    _result = "CIN";
                    break;
                case TransactionType.Tax:
                    _result = "TAX";
                    break;
                case TransactionType.CrewAssignment:
                    _result = "CA";
                    break;
                case TransactionType.PPHT:
                    _result = "PRE";
                    break;
                case TransactionType.Customer:
                    _result = "C";
                    break;
                case TransactionType.FAAddStock:
                    _result = "FAW";
                    break;
                case TransactionType.FAService:
                    _result = "FAS";
                    break;
                case TransactionType.PackingList:
                    _result = "PCK";
                    break;
                case TransactionType.Tally:
                    _result = "TAL";
                    break;
                case TransactionType.HRMRecruitmentRequest:
                    _result = "HRR";
                    break;
                case TransactionType.BankRecon:
                    _result = "BRC";
                    break;
                case TransactionType.ApplicantResume:
                    _result = "RSM";
                    break;
                case TransactionType.MoneyChanger:
                    _result = "MNC";
                    break;
                case TransactionType.ApplicantScreening:
                    _result = "SCR";
                    break;
                case TransactionType.HRMScreeningSchedule:
                    _result = "HSS";
                    break;
                case TransactionType.HRMApplicantFinishing:
                    _result = "HAF";
                    break;
                case TransactionType.HRMScreeningProcess:
                    _result = "HSP";
                    break;
                case TransactionType.HRMAbsenceRequest:
                    _result = "HAR";
                    break;
                case TransactionType.HRMUpdateLeave:
                    _result = "HUL";
                    break;
                case TransactionType.HRMTerminationRequest:
                    _result = "HTR";
                    break;
                case TransactionType.HRMTerminationFinishing:
                    _result = "HTF";
                    break;
                case TransactionType.HRMExitInterview:
                    _result = "HEI";
                    break;
                case TransactionType.PurchaseRetur:
                    _result = "RP";
                    break;
                case TransactionType.Retail:
                    _result = "SAL";
                    break;
                case TransactionType.NCPSales:
                    _result = "NCS";
                    break;
                case TransactionType.NCPRefund:
                    _result = "NCR";
                    break;
                case TransactionType.ProductAssembly:
                    _result = "SPA";
                    break;
                case TransactionType.DirectSales:
                    _result = "SDS";
                    break;
                case TransactionType.SalesConfirmation:
                    _result = "SC";
                    break;
                case TransactionType.Contract:
                    _result = "CTR";
                    break;
                case TransactionType.DirectPurchase:
                    _result = "PDP";
                    break;
                case TransactionType.FixedAssetPurchaseOrder:
                    _result = "FPO";
                    break;
                case TransactionType.ClaimIssue:
                    _result = "HCI";
                    break;
                case TransactionType.HRMTrDinas:
                    _result = "HDS";
                    break;
                case TransactionType.ClaimPlafon:
                    _result = "HCP";
                    break;
                case TransactionType.HRMLembur:
                    _result = "OVT";
                    break;
                case TransactionType.HRMLoanIn:
                    _result = "HLI";
                    break;
                case TransactionType.HRMLeaveAdd:
                    _result = "HLA";
                    break;
                case TransactionType.HRMTerm:
                    _result = "HTE";
                    break;
                case TransactionType.PAYBenefit:
                    _result = "BNF";
                    break;
                case TransactionType.PAYDeduction:
                    _result = "DDT";
                    break;
                case TransactionType.PAYSalary:
                    _result = "SLR";
                    break;
                case TransactionType.ScheduleShift:
                    _result = "SCS";
                    break;
                case TransactionType.EmpGroupAssign:
                    _result = "EGA";
                    break;
                case TransactionType.ServiceInvWithoutWarranty:
                    _result = "SWW";
                    break;
                case TransactionType.NCCSparepartRequest:
                    _result = "SPR";
                    break;
                case TransactionType.PAYSlip:
                    _result = "SLP";
                    break;
                case TransactionType.PAYPayroll:
                    _result = "PAY";
                    break;
                case TransactionType.NCCReturSparepart:
                    _result = "RSP";
                    break;
                case TransactionType.PAYPPhYear:
                    _result = "PPY";
                    break;
                case TransactionType.LeaveRequest:
                    _result = "HLR";
                    break;
                case TransactionType.DirectBillOfLading:
                    _result = "DSJ";
                    break;
                case TransactionType.EmpReward:
                    _result = "RWD";
                    break;
                case TransactionType.EmpReprimand:
                    _result = "RPM";
                    break;
                case TransactionType.THR:
                    _result = "THR";
                    break;
                case TransactionType.RadiusUpdateVoucher:
                    _result = "RUV";
                    break;
                case TransactionType.Appraisal:
                    _result = "APS";
                    break;
                case TransactionType.SalaryPayment:
                    _result = "PYS";
                    break;
                case TransactionType.Clinic:
                    _result = "CLN";
                    break;
                case TransactionType.MedicalRecordNumber:
                    _result = "MR";
                    break;
                case TransactionType.HRMChangeShift:
                    _result = "HCS";
                    break;
                case TransactionType.SalarySK:
                    _result = "SK";
                    break;
                case TransactionType.POSRetail:
                    _result = "RTL";
                    break;
                case TransactionType.POSInternet:
                    _result = "INT";
                    break;
                case TransactionType.POSSettlement:
                    _result = "PST";
                    break;
                case TransactionType.POSSettlementDP:
                    _result = "DP";
                    break;
                case TransactionType.AdjustDiffRate:
                    _result = "ADF";
                    break;
                case TransactionType.SuppInvConsignment:
                    _result = "SIC";
                    break;
                case TransactionType.BillingSupplierInvoice:
                    _result = "SIN";
                    break;
                case TransactionType.Ticketing:
                    _result = "TKT";
                    break;
                case TransactionType.Hotel:
                    _result = "HTL";
                    break;
                case TransactionType.HRMLoanChange:
                    _result = "HLC";
                    break;
                case TransactionType.HRMLateDispensation:
                    _result = "HLD";
                    break;
                case TransactionType.StockServiceIn:
                    _result = "SSI";
                    break;
                case TransactionType.StockServiceOut:
                    _result = "SSO";
                    break;
                case TransactionType.StockDeliveryRetur:
                    _result = "RSJ";
                    break;
                case TransactionType.BILTrRadiusActivateTemporary:
                    _result = "BAT";
                    break;
                case TransactionType.CogsProcess:
                    _result = "CGS";
                    break;
                case TransactionType.CustomerRetur:
                    _result = "FCR";
                    break;
                case TransactionType.SupplierRetur:
                    _result = "FSR";
                    break;
            }
            return _result;
        }

        public static string GetValueName(String _prmTransType)
        {
            string _result = "";

            switch (_prmTransType)
            {
                case "TRN":
                    _result = "Transaction";
                    break;
                case "JE":
                    _result = "Journal Entry";
                    break;
                case "PCO":
                    _result = "Petty Cash";
                    break;
                case "FAB":
                    _result = "FA Beginning";
                    break;
                case "FAO":
                    _result = "FADevaluation";
                    break;
                case "FAM":
                    _result = "FAMoving";
                    break;
                case "PYN":
                    _result = "Payment Non Purchase";
                    break;
                case "PYT":
                    _result = "Payment Trade";
                    break;
                case "FAD":
                    _result = "FA Process";
                    break;
                case "FAJ":
                    _result = "FA Sales";
                    break;
                case "FAT":
                    _result = "FA Tenancy";
                    break;
                case "ORN":
                    _result = "Receipt Non Trade";
                    break;
                case "DNS":
                    _result = "NotaDebit Supplier";
                    break;
                case "CNS":
                    _result = "NotaCredit Supplier";
                    break;
                case "CNC":
                    _result = "NotaCredit Customer";
                    break;
                case "DNC":
                    _result = "NotaDebit Customer";
                    break;
                case "ORT":
                    _result = "Receipt Trade";
                    break;
                case "CGO":
                    _result = "Change Giro Payment";
                    break;
                case "DPL":
                    _result = "FINDP Cust List";
                    break;
                case "CGI":
                    _result = "Change Giro Receipt";
                    break;
                case "DPC":
                    _result = "DP Customer";
                    break;
                case "DCR":
                    _result = "FINDP Cust Retur";
                    break;
                case "DPS":
                    _result = "DP Supp Pay";
                    break;
                case "DSR":
                    _result = "FINDP Supp Retur";
                    break;
                case "APR":
                    _result = "AP Rate";
                    break;
                case "ARR":
                    _result = "AR Rate";
                    break;
                case "SIR":
                    _result = "Stock Issue Request";
                    break;
                case "SIF":
                    _result = "Stock Issue Request FA";
                    break;
                case "STR":
                    _result = "Stock Trans Request";
                    break;
                case "FAP":
                    _result = "FA Purchase";
                    break;
                case "SRR":
                    _result = "Request Sales Retur";
                    break;
                case "SRQ":
                    _result = "Purchase Request";
                    break;
                case "SAD":
                    _result = "Stock Adjustment";
                    break;
                case "SSD":
                    _result = "Stock Beginning";
                    break;
                case "SOP":
                    _result = "Stock Opname";
                    break;
                case "SFA":
                    _result = "Stock Issue To FA";
                    break;
                case "SSR":
                    _result = "Stock Receiving Retur";
                    break;
                case "SPO":
                    _result = "Stock Reject Out";
                    break;
                case "SPI":
                    _result = "Stock Reject In";
                    break;
                case "STI":
                    _result = "Stock Transfer External RR";
                    break;
                case "STO":
                    _result = "Stock Transfer External SJ";
                    break;
                case "SRO":
                    _result = "Stock Receiving Other";
                    break;
                case "SRC":
                    _result = "Stock Receiving Customer";
                    break;
                case "SRS":
                    _result = "Stock Receiving Supplier";
                    break;
                case "STG":
                    _result = "Stock Transfer Internal";
                    break;
                case "SWI":
                    _result = "Stock Issue Slip";
                    break;
                case "SCH":
                    _result = "Stock Change Good";
                    break;
                case "SO":
                    _result = "Sales Order";
                    break;
                case "DO":
                    _result = "Delivery Order";
                    break;
                case "SJ":
                    _result = "Bill Of Lading";
                    break;
                case "PCR":
                    _result = "Petty Cash Receive";
                    break;
                case "PO":
                    _result = "Purchase Order";
                    break;
                case "RR":
                    _result = "Receiving PO";
                    break;
                case "CI":
                    _result = "Customer Note";
                    break;
                case "SI":
                    _result = "Supplier Note";
                    break;
                case "INV":
                    _result = "Billing Invoice";
                    break;
                case "CBA":
                    _result = "Customer Billing Account";
                    break;
                case "CIN":
                    _result = "Customer Invoice";
                    break;
                case "TAX":
                    _result = "Tax";
                    break;
                case "CA":
                    _result = "Crew Assignment";
                    break;
                case "PRE":
                    _result = "PPHT";
                    break;
                case "C":
                    _result = "Customer";
                    break;
                case "FAW":
                    _result = "FA Add Stock";
                    break;
                case "FAS":
                    _result = "FA Service";
                    break;
                case "PCK":
                    _result = "Packing List";
                    break;
                case "TAL":
                    _result = "Tally";
                    break;
                case "HRR":
                    _result = "HRM Recruitment Request";
                    break;
                case "BRC":
                    _result = "Bank Recon";
                    break;
                case "RSM":
                    _result = "Applicant Resume";
                    break;
                case "MNC":
                    _result = "Money Changer";
                    break;
                case "SCR":
                    _result = "Applicant Screening";
                    break;
                case "HSS":
                    _result = "HRM Screening Schedule";
                    break;
                case "HAF":
                    _result = "HRM Applicant Finishing";
                    break;
                case "HSP":
                    _result = "HRM Screening Process";
                    break;
                case "HAR":
                    _result = "HRM Absence Request";
                    break;
                case "HUL":
                    _result = "HRM Update Leave";
                    break;
                case "HTR":
                    _result = "HRM Termination Request";
                    break;
                case "HTF":
                    _result = "HRM Termination Finishing";
                    break;
                case "HEI":
                    _result = "HRM Exit Interview";
                    break;
                case "RP":
                    _result = "Purchase Retur";
                    break;
                case "SAL":
                    _result = "Retail";
                    break;
                case "NCS":
                    _result = "NCP Sales";
                    break;
                case "NCR":
                    _result = "NCP Refund";
                    break;
                case "SPA":
                    _result = "Product Assembly";
                    break;
                case "SDS":
                    _result = "Direct Sales";
                    break;
                case "SC":
                    _result = "Sales Confirmation";
                    break;
                case "CTR":
                    _result = "Contract";
                    break;
                case "PDP":
                    _result = "Direct Purchase";
                    break;
                case "FPO":
                    _result = "Fixed Asset Purchase Order";
                    break;
                case "HCI":
                    _result = "Claim Issue";
                    break;
                case "HDS":
                    _result = "HRM Tr Dinas";
                    break;
                case "HCP":
                    _result = "Claim Plafon";
                    break;
                case "OVT":
                    _result = "HRM Lembur";
                    break;
                case "HLI":
                    _result = "HRM Loan In";
                    break;
                case "HLA":
                    _result = "HRM Leave Add";
                    break;
                case "HTE":
                    _result = "HRM Term";
                    break;
                case "BNF":
                    _result = "PAY Benefit";
                    break;
                case "DDT":
                    _result = "PAY Deduction";
                    break;
                case "SLR":
                    _result = "PAY Salary";
                    break;
                case "SCS":
                    _result = "Schedule Shift";
                    break;
                case "EGA":
                    _result = "Emp Group Assign";
                    break;
                case "SWW":
                    _result = "Service Inv Without Warranty";
                    break;
                case "SPR":
                    _result = "NCC Sparepart Request";
                    break;
                case "SLP":
                    _result = "PAY Slip";
                    break;
                case "PAY":
                    _result = "PAY Payroll";
                    break;
                case "RSP":
                    _result = "NCC Retur Sparepart";
                    break;
                case "PPY":
                    _result = "PAY PPh Year";
                    break;
                case "HLR":
                    _result = "Leave Request";
                    break;
                case "DSJ":
                    _result = "Direct Bill Of Lading";
                    break;
                case "RWD":
                    _result = "Emp Reward";
                    break;
                case "RPM":
                    _result = "Emp Reprimand";
                    break;
                case "THR":
                    _result = "THR";
                    break;
                case "RUV":
                    _result = "Radius Update Voucher";
                    break;
                case "APS":
                    _result = "Appraisal";
                    break;
                case "PYS":
                    _result = "Salary Payment";
                    break;
                case "CLN":
                    _result = "Clinic";
                    break;
                case "MR":
                    _result = "Medical Record Number";
                    break;
                case "HCS":
                    _result = "HRM Change Shift";
                    break;
                case "SK":
                    _result = "Salary SK";
                    break;
                case "RTL":
                    _result = "POS Retail";
                    break;
                case "INT":
                    _result = "POS Internet";
                    break;
                case "PST":
                    _result = "POS Settlement";
                    break;
                case "DP":
                    _result = "POS Settlement DP";
                    break;
                case "ADF":
                    _result = "Adjust DiffRate";
                    break;
                case "SIC":
                    _result = "Supp Inv Consignment";
                    break;
                case "SIN":
                    _result = "Billing Supplier Invoice";
                    break;
                case "TKT":
                    _result = "Ticketing";
                    break;
                case "HTL":
                    _result = "Hotel";
                    break;
                case "HLD":
                    _result = "HRM Late Dispensation";
                    break;
            }

            return _result;
        }
    }
}