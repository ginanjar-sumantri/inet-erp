using System;

namespace InetGlobalIndo.ERP.MTJ.Common.Enum
{
    public enum IsPIC : byte
    {
        Yes = 1,
        No = 0
    }

    public enum IsDeleted : byte
    {
        Yes = 1,
        No = 0
    }

    public enum YesNo : byte
    {
        Yes = 1,
        No = 0
    }

    public enum TransCloseStatus : byte
    {
        Locked = 1,
        Open = 0
    }

    public enum DateForm : byte
    {
        SQL,
        English,
        Indonesian
    }

    public enum ConnectionType : byte
    {
        Development = 6,
        Testing = 3,
        Production = 10
    }

    public enum CurrDefAcc : byte
    {
        AccountPayable = 4,
        AccountReceiveable = 3,
        SalesDiscount = 2,
        RealizedGainOrLoss = 1,
        UnRealizedGainOrLoss = 0
    }

    public enum ToDoAction : byte
    {
        Add = 1,
        Edit = 2,
        Delete = 3,
        None = 4
    }

    public enum ERPModule : byte
    {
        Home = 5,
        Purchasing = 8,
        Payroll = 6,
        Accounting = 1,
        Finance = 3,
        StockControl = 12,
        HumanResource = 4,
        ExecutiveInformation = 2,
        Production = 7,
        Sales = 9,
        ShipPorting = 11,
        Settings = 10,
        Billing = 13,
        NCC = 14,
        SMS = 15,
        Clinic = 16,
        POS = 17,
        Tour = 18
    }

    public enum TransactionType : byte
    {
        JournalEntry = 1,
        PettyCash = 2,
        FADevaluation = 3,
        FAMoving = 4,
        FAProcess = 5,
        PaymentNonPurchase = 6,
        FATenancy = 7,
        FASales = 8,
        ReceiptNonTrade = 9,
        NotaDebitSupplier = 10,
        NotaCreditSupplier = 11,
        NotaCreditCustomer = 12,
        NotaDebitCustomer = 13,
        PaymentTrade = 14,
        ReceiptTrade = 15,
        FINDPCustList = 16,
        ChangeGiroPayment = 17,
        ChangeGiroReceipt = 18,
        DPCustomer = 19,
        FINDPCustRetur = 20,
        DPSuppPay = 21,
        FINDPSuppRetur = 22,
        ARRate = 23,
        APRate = 24,
        StockIssueRequest = 25,
        StockTransRequest = 26,
        FAPurchase = 27,
        RequestSalesRetur = 28,
        PurchaseRequest = 29,
        BudgetEntry = 30,
        StockOpname = 31,
        StockAdjustment = 32,
        StockBeginning = 33,
        StockIssueToFA = 34,
        StockReceivingRetur = 35,
        StockIssueRequestFA = 36,
        StockRejectOut = 37,
        StockTransferExternalRR = 38,
        StockTransferExternalSJ = 39,
        StockRejectIn = 40,
        StockReceivingOther = 41,
        StockReceivingCustomer = 42,
        StockReceivingSupplier = 43,
        StockTransferInternal = 44,
        StockIssueSlip = 45,
        StockChangeGood = 46,
        SalesOrder = 47,
        DeliveryOrder = 48,
        BillOfLading = 49,
        PettyCashReceive = 50,
        PurchaseOrder = 51,
        ReceivingPO = 52,
        CustomerNote = 53,
        SupplierNote = 54,
        BillingInvoice = 55,
        CustomerBillingAccount = 56,
        CustomerInvoice = 57,
        Tax = 58,
        Transaction = 59,
        CrewAssignment = 60,
        PPHT = 61,
        Customer = 62,
        FAAddStock = 63,
        FAService = 64,
        PackingList = 65,
        Tally = 66,
        HRMRecruitmentRequest = 67,
        BankRecon = 68,
        ApplicantResume = 69,
        MoneyChanger = 70,
        ApplicantScreening = 71,
        HRMScreeningSchedule = 72,
        FABeginning = 73,
        HRMApplicantFinishing = 74,
        HRMScreeningProcess = 75,
        HRMAbsenceRequest = 76,
        HRMUpdateLeave = 77,
        HRMTerminationRequest = 78,
        HRMTerminationFinishing = 79,
        HRMExitInterview = 80,
        PurchaseRetur = 81,
        Retail = 82,
        NCPSales = 83,
        NCPRefund = 84,
        ProductAssembly = 85,
        DirectSales = 86,
        SalesConfirmation = 87,
        Contract = 88,
        DirectPurchase = 89,
        FixedAssetPurchaseOrder = 90,
        ClaimIssue = 91,
        HRMTrDinas = 92,
        ClaimPlafon = 93,
        HRMLembur = 94,
        HRMLoanIn = 95,
        HRMLeaveAdd = 96,
        HRMTerm = 97,
        NCCSparepartRequest = 98,
        PAYBenefit = 99,
        PAYDeduction = 100,
        PAYSalary = 101,
        ScheduleShift = 102,
        EmpGroupAssign = 103,
        ServiceInvWithoutWarranty = 104,
        PAYSlip = 105,
        PAYPayroll = 106,
        NCCReturSparepart = 107,
        PAYPPhYear = 108,
        LeaveRequest = 109,
        DirectBillOfLading = 110,
        RadiusUpdateVoucher = 111,
        EmpReward = 112,
        EmpReprimand = 113,
        THR = 114,
        Appraisal = 115,
        SalaryPayment = 116,
        Clinic = 117,
        MedicalRecordNumber = 118,
        HRMChangeShift = 119,
        SalarySK = 120,
        POSRetail = 121,
        POSInternet = 122,
        POSSettlement = 123,
        POSSettlementDP = 124,
        AdjustDiffRate = 125,
        SuppInvConsignment = 126,
        BillingSupplierInvoice = 127,
        Ticketing = 128,
        Hotel = 129,
        HRMLoanChange = 130,
        HRMLateDispensation = 131,
        StockServiceIn = 132,
        StockServiceOut = 133,
        StockDeliveryRetur = 134,
        BILTrRadiusActivateTemporary = 135,
        CogsProcess = 136,
        CustomerRetur = 137,
        SupplierRetur= 138
    }

    //public enum SearchType : byte
    //{
    //    All = 0,
    //    StartWith = 1,
    //    EndWith = 2,
    //    Contains = 3
    //}

    public enum TransStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3,
        Deleted = 4,
        Complete =5
    }

    public enum ClinicStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum ConsignmentStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum AppResumeStatus : byte
    {
        Draft = 0,
        WaitingForApproval = 1,
        Approved = 2
    }

    public enum ScreeningStatus : byte
    {
        Draft = 0,
        WaitingForApproval = 1,
        Approved = 2
    }

    public enum JEStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum PCOStatus : byte
    {
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3,
        OnHold = 4
    }

    public enum ChangeType : byte
    {
        Add = 1,
        Change = 2,
        Transfer = 3,
        Remove = 4
    }

    public enum MoneyChangerStatus : byte
    {
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3,
        OnHold = 4
    }

    public enum PettyCashReceiveType : byte
    {
        Petty = 1,
        Payment = 2
    }

    public enum MoneyChangerType : byte
    {
        Petty = 1,
        Payment = 2
    }

    public enum FAStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum FAPurchaseStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum FAProcessStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum FAServiceStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum PaymentNonPurchaseStatus : byte
    {
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3,
        OnHold = 4
    }

    public enum Language : byte
    {
        Indonesia = 0,
        English = 1
    }

    public enum FAMoving : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum SupplierStatus : byte
    {
        Active = 1,
        NotActive = 2
    }

    public enum ModePaymentType : byte
    {
        Bank = 1,
        Giro = 2,
        DP = 3,
        Kas = 4,
        Other = 5
    }

    public enum ModeReceiptType : byte
    {
        Bank = 1,
        Giro = 2,
        DP = 3,
        Kas = 4,
        Other = 5
    }

    public enum TypePayment : byte
    {
        Payment = 1,
        Receipt = 2,
        All = 3
    }

    public enum TypeReceipt : byte
    {
        Payment = 1,
        Receipt = 2,
        All = 3
    }

    public enum PaymentNonTradeStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum PaymentTradeStatus : byte
    {
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3,
        OnHold = 4
    }

    public enum ReceiptTradeStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum ReceiptNonTradeStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum NotaDebitSupplierStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }
    public enum StockReceivingReturStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum NotaCreditSupplierStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum NotaCreditCustomerStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum NotaDebitCustomerStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum GiroReceiptChangeStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum GiroReceiptStatus : byte
    {
        OnHold = 1,
        Deposit = 2,
        Drawn = 3,
        Cancelled = 4,
        Change = 5
    }

    public enum GiroPaymentChangeStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum GiroPaymentStatus : byte
    {
        OnHold = 1,
        Change = 2,
        Drawn = 3,
        Cancelled = 4
    }

    public enum DPCustomerListStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum DPCustomerStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum SuppDiffRateStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum DPSuppPayStatus : byte
    {
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3,
        OnHold = 4
    }

    public enum DPCustomerReturStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum DPSupplierReturStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum CustBeginningStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum SuppBeginningStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum ARRateStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum APRateStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum GLBudgetStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockTransRequestStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockTransRequestStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum StockIssueRequestStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum StockIssueRequestStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockIssueRequestFAStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum StockIssueRequestFAStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum RequestSalesReturStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum RequestSalesReturStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum PurchaseRequestStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum PurchaseRequestStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum PlanShipApprovalStatus : byte
    {
        PortChiefUnit = 1,
        OperationManager = 2
    }

    public enum ShipApprovalStatus : byte
    {
        OperationManager = 1,
        OrderedBy = 2
    }

    public enum AccountType : byte
    {
        ProfitLost,
        BalanceSheet
    }

    public enum StockOpnameStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockAdjustmentStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockIssueFixedAssetStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockRejectOutStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockRejectInStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockBeginningStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockTransferExternalRRStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockTransferExternalSJStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum WarehouseSubledStatus : byte
    {
        Customer = 1,
        Supplier = 2,
        NoSubled = 3
    }

    public enum StockReceivingOtherStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockReceivingCustomerStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockReceivingSupplierStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum BillOfLadingStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum DirectBillOfLadingStatus : byte
    {
        OnHold = 1,
        WaitingForApproval = 2,
        Approved = 3,
        Posted = 4
    }

    public enum RRTypeStatus : byte
    {
        Customer = 1,
        Supplier = 2,
        Other = 3
    }

    public enum StockTransferInternalStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockIssueSlipStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum StockChangeGoodStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum PettyType : byte
    {
        Payment = 1,
        Receipt = 2
    }

    public enum SalesOrderStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum SalesOrderStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum SparepartRequestStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum ReturSparepartStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum ServiceInvStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum DeliveryOrderStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum DeliveryOrderStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum PurchaseOrderStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum FixedAssetPurchaseOrderStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum ReceivingPOStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum CustomerNoteStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum SupplierNoteStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum SetupStatus : byte
    {
        Account = 1,
        AutoNmbr = 2
    }

    public enum PurchaseOrderStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum FixedAssetPurchaseOrderStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum DocServiceStatus : byte
    {
        OnHold = 0,
        InProgress = 1,
        Cancelled = 2,
        Done = 3
    }

    public enum DocServiceActorStatus : byte
    {
        Created = 0,
        GetApproval = 1,
        Approved = 2,
        Posted = 3,
        Unposted = 4
    }

    public enum BillingInvoiceStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum SubledStatus : byte
    {
        NoSubled = 1,
        Customer = 2,
        Supplier = 3,
        Employee = 4,
        FixedAsset = 5,
        Product = 6
    }

    public enum CustomerInvoiceStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum CustomerInvoiceType : byte
    {
        Other = 1,
        Postpone = 2,
        Register = 3,
        SecurityDeposit = 4
    }

    public enum CustomerBillAccount : byte
    {
        PraBayar = 1,
        PascaBayar = 2
    }

    public enum FAAddStockStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum SupplierInvoiceStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum TaxTransactionCode : byte
    {
        One = 0,    //NB: Penyerahan kepada selain Pemungut PPN
        Two = 1,    //NB: Penyerahan kepada Pemungut PPN Bendaharawan Pemerintah
        Three,      //NB: Penyerahan kepada Pemungut PPN Lainnya (selain Bendaharawan Pemerintah)
        Four,       //NB: Penyerahan yang menggunakan DPP Nilai Lain kepada selain Pemungut PPN;
        Five,       //NB: Penyerahan yang Pajak Masukannya diDeemed kepada selain Pemungut PPN;
        Six,        //NB: Penyerahan Lainnya kepada selain Pemungut PPN;
        Seven,      //NB: Penyerahan yang PPN atau PPN dan PPn BM-nya Tidak Dipungut kepada selain Pemungut PPN;
        Eight,      //NB: Digunakan untuk penyerahan yang Dibebaskan dari pengenaan PPN atau PPN dan PPn BM kepada selain Pemungut PPN;
        Nine        //NB: Digunakan untuk penyerahan Aktiva Pasal 16D kepada selain Pemungut PPN
    }

    public enum TaxStatus : byte
    {
        Zero = 0,   //NB: Normal
        One = 1     //NB: Penggantian
    }

    public enum JobLevel : byte
    {
        Nahkoda = 0,
        Mualim = 1,
        ABK = 2
    }

    public enum PPHTStatus : byte
    {
        OnHold = 0,
        InProgress = 1,
        Cancelled = 2,
        Done = 3
    }

    public enum PPHTActorStatus : byte
    {
        Created = 0,
        GetApproval = 1,
        Approved = 2,
        Posted = 3,
        Unposted = 4
    }

    public enum PackingListStatus : byte
    {
        OnHold = 0,
        InProgress = 1,
        Cancelled = 2,
        Done = 3
    }

    public enum PackingListActorStatus : byte
    {
        Created = 0,
        GetApproval = 1,
        Approved = 2,
        Posted = 3,
        Unposted = 4
    }

    public enum CrewAssignmentActorStatus : byte
    {
        Created = 0,
        GetApproval = 1,
        Approved = 2,
        Posted = 3,
        Unposted = 4
    }

    public enum CrewAssignmentStatus : byte
    {
        OnHold = 0,
        InProgress = 1,
        Cancelled = 2,
        Done = 3
    }

    public enum FixedAssetCreatedFrom : byte
    {
        FAAddStock = 1,
        FAPurchase = 2,
        Manual = 3
    }

    public enum Action : byte
    {
        Add,
        Edit,
        Delete,
        View,
        GetApproval,
        Approve,
        Posting,
        Unposting,
        PrintPreview,
        TaxPreview,
        Close,
        Access,
        Generate,
        Revisi,
        Complete
    }

    public enum OrganizationUnitActiveStatus : byte
    {
        Active = 1,
        NotActive = 2
    }

    public enum TallyStatus : byte
    {
        OnHold = 0,
        InProgress = 1,
        Cancelled = 2,
        Done = 3
    }

    public enum TallyActorStatus : byte
    {
        Created = 0,
        GetApproval = 1,
        Approved = 2,
        Posted = 3,
        Unposted = 4
    }

    public enum RecruitmentRequestStatus : byte
    {
        Draft = 1,
        WaitingForApproval = 2,
        Approved = 3,
        Closed = 4
    }

    public enum AdjustType : byte
    {
        Saldo = 0,
        Adjust = 1
    }

    public enum StockRequestType : byte
    {
        RequestStock = 0,
        RequestFA = 1
    }

    public enum BankReconStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum NCPRefundStatus : byte
    {
        OnHold = 3,
        WaitingForApproval = 1,
        Approved = 4,
        Posted = 2
    }
    public enum FABeginningStatus : byte
    {
        WaitingForApproval = 1,
        Posted = 2,
        OnHold = 3,
        Approved = 4
    }

    public enum ScreeningScheduleStatus : byte
    {
        Draft = 1,
        WaitingForConfirmation = 2,
        Confirmed = 3,
        Closed = 4
    }

    public enum ScreeningProcessStatus : byte
    {
        Draft = 1,
        WaitingForApproval = 2,
        Approved = 3
    }

    public enum ScreeningStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum TerminationRequestStatus : byte
    {
        Draft = 1,
        WaitingForApproval = 2,
        Approved = 3,
        Closed = 4,
        Cancelled = 5,
        Rejected = 6
    }

    public enum TerminationHandOverStatus : byte
    {
        Open = 1,
        OnProgress = 2,
        Closed = 3
    }

    public enum ApplicantFinishingStatus : byte
    {
        Draft = 1,
        WaitingForApproval = 2,
        Approved = 3,
        Closed = 4
    }

    public enum AbsenceRequestStatus : byte
    {
        Draft = 1,
        WaitingForApproval = 2,
        Approved = 3,
        Posted = 4
    }

    public enum LeaveRequestStatus : byte
    {
        OnHold = 1,
        WaitingForApproval = 2,
        Approved = 3,
        Posted = 4
    }

    public enum UpdateLeaveStatus : byte
    {
        Draft = 1,
        WaitingForApproval = 2,
        Approved = 3
    }

    public enum TerminationFinishingStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum ExitInterviewStatus : byte
    {
        Draft = 1,
        WaitingForApproval = 2,
        Approved = 3
    }

    public enum PermissionLevel : byte
    {
        NoAccess = 1,
        Self = 2,
        SelfOU = 3,
        InheritOU = 4,
        EntireOU = 5
    }

    public enum ProductValType : byte
    {
        FIFO = 0,
        LIFO = 1,
        Average = 2
    }

    public enum PurchaseReturStatus : byte
    {
        OnHold = 1,
        WaitingForApproval = 2,
        Approved = 3,
        Posted = 4
    }

    public enum FinancialRatioType : byte
    {
        CurrentRatio = 0,
        QuickRatio = 1,
        AssetTurnover = 2,
        InventoryTurnover = 3,
        AccountsReceivableTurnover = 4,
        AverageCollectionPeriod = 5,
        GrossProfitMargin = 6,
        NetProfitMargin = 7,
        ReturnOnEquity = 8,
        ReturnOnAsset = 9,
        DividendPayoutRatio = 10,
        DebtToAssetRatio = 11,
        DebtToEquityRatio = 12,
        NetWorkingCapital = 13,
        DaysSalesOutstandingRatio = 14,
        TimesInterestEarnedRatio = 15
    }

    public enum FinancialRatioAcc : byte
    {
        CurrentAssets = 0,
        CurrentLiabilities = 1,
        TotalAssets = 2,
        TotalLiabilities = 3,
        AccountReceiveable = 4,
        Inventories = 5,
        Prepayments = 6,
        LongTermDebt = 7,
        ValueOfLeases = 8,
        ShareholdersEquity = 9,
        Sales = 10,
        COGS = 11,
        Devidend = 12,
        InterestExpense = 13
    }

    public enum GroupLevel : byte
    {
        AccountType = 0,
        AccountGroup = 1,
        AccountSubGroup = 2,
        AccountClass = 3,
        Account = 4
    }

    public enum ActivityType : byte
    {
        Add = 0,
        Edit = 1,
        Delete = 2,
        GetApproval = 3,
        Approve = 4,
        Posting = 5,
        UnPosting = 6,
        Close = 7,
        Deleted = 8
    }

    public enum ReportType : byte
    {
        Report = 0,
        PrintPreview = 1
    }

    public enum RetailStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2
    }

    public enum NCPSalesStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum ProductAssemblyStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum CompanyConfigure : byte
    {
        FACodeAutoNumber = 0,
        FACodeDigitNumber = 1,
        COGSMethod = 2,
        FADepreciationMethod = 3,
        BOLReferenceType = 4,
        ViewJobTitlePrintReport = 5,
        ActivePGYear = 6,
        FlyerEmail = 7,
        BillingAuthorizedSignImage = 8,
        LeaveTypeProcess = 9,
        PremiGroupBy = 10,
        RDLCBillingInvoiceSendEmail = 11,
        BillingHeaderImage = 12,
        BillingLeftImage = 13,
        BillingFooterImage = 14,
        SalaryEncryption = 15,
        SalaryEncryptionKeyValidation = 16,
        SalaryEncryptionKey = 17,
        Theme = 18,
        PosisiLogo = 19,
        PayrollAuthorizedSignImage = 20,
        IgnoreItemDiscount = 21,
        POSServiceCharge = 22,
        POSTaxCharge = 23,
        POSRounding = 24,
        AutoNmbrLocForFA = 25,
        AutoNmbrPeriodForFA = 26,
        HaveProductItemDuration = 27,
        POSBookingTimeLimitAfter = 28,
        POSBookingTimeLimitBefore = 29,
        EmailFromSlipPayment = 30,
        EmailBodySlipPayment = 31,
        LeaveCanTakeLeaveAfterWorkTime = 32,
        POSCafeTimeLimitAfter = 33,
        POSInternetTimeLimitAfter = 34,
        BillingRadiusToleranceDay = 35
    }

    public enum ThemeComponent : byte
    {
        ThemeName = 0,
        ThemeCode = 1,
        BackgroundColorBody = 2,
        BackgroundImage = 3,
        BackgroundImageBawah = 4,
        RowColor = 5,
        RowColorAlternate = 6,
        RowColorHover = 7,
        WelcomeTextColor = 8,
        BackgroundColorLogin = 9,
        BackgroundimageLogin = 10,
        BackgroundImagePanelLogin = 11,
        PanelLoginWidth = 12,
        PanelLoginHeight = 13
    }

    public enum FACodeAutoNumber : byte
    {
        False = 0,
        True = 1
    }

    public enum DirectSalesStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum DirectPurchaseStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum ContractStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum UnSubStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum SalesConfirmationStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum SalesConfirmationPendingStatus : byte
    {
        NotPending = 0,
        Pending = 1
    }

    public enum EmailNotificationType : byte
    {
        Internal = 0,
        External = 1
    }

    public enum TermAndConditionType : byte
    {
        SalesConfirmation = 0,
        Contract = 1
    }

    public enum EmailNotificationID : byte
    {
        SPKInstallasi = 0,
        ContractLetterNotYetApproved = 1,
        ContractLetterNotYetApprovedInternal = 2,
        BANotYetApproved = 3,
        BANotYetApprovedInternal = 4,
        BASoftBlock = 5,
        SPKBASoftBlock = 6,
        BAOpenSoftBlock = 7,
        SPKBAOpenSoftBlock = 8,
        CLSoftBlock = 9,
        SPKCLSoftBlock = 10,
        CLOpenSoftBlock = 11,
        SPKCLOpenSoftBlock = 12,
        NotPay = 13,
        NotPaySoftBlock = 14,
        SPKNotPaySoftBlock = 15,
        NotPayOpenSoftBlock = 16,
        SPKNotPayOpenSoftBlock = 17,
        NotPaySoftBlockInternal = 18,
        NotPayOpenSoftBlockInternal = 19,
        BillingInvoiceEmailNotYetSent = 20
    }

    public enum BeritaAcaraStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum CheckPlafonType : byte
    {
        All = 0,
        EmployeeID = 1,
        JobLevel = 2,
        JobTitle = 3,
        WorkPlace = 4,
        EmployeeType = 5
    }

    public enum DayTypeStatus : byte
    {
        Work = 0,
        Holiday = 1,
        PublicHoliday = 2
    }

    public enum ClaimForStatus : byte
    {
        All = 0,
        Employee = 1,
        Family = 2
    }

    public enum ClaimIssueStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum DinasStatus : byte
    {
        Draft = 1,
        WaitingForApproval = 2,
        Approved = 3,
        Posted = 4
    }

    public enum LeaveProcessStatus : byte
    {
        Draft = 1,
        WaitingForApproval = 2,
        Approved = 3,
        Posted = 4
    }

    public enum ClaimPlafonStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum LoanInStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum LoanInStatusDt : byte
    {
        Closed = 1,
        Open = 2
    }

    public enum LemburStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum LemburDtStatus : byte
    {
        OnHold = 0,
        Cancel = 1,
        Complete = 2
    }

    public enum ScheduleType : byte
    {
        Shift = 0,
        Office = 1
    }

    public enum LeaveTypeProcessType : byte
    {
        Monthly = 0,
        Yearly = 1
    }

    public enum LeaveAddStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum HRMTermStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum MethodRangeType : byte
    {
        Weekly = 0,
        Monthly = 1,
        Biweekly = 2
    }

    public enum AllowanceType : byte
    {
        MasaKerja = 0,
        THR = 1,
        Penghargaan = 2,
        Pesangon = 3
    }

    public enum BaseOn : byte
    {
        Salary = 0,
        Nominal = 1
    }

    public enum PayrollPref : byte
    {
        Benefit = 0,
        Deduction = 1,
        Payroll = 2,
        Terminate = 3
    }

    public enum Tertanggung : byte
    {
        Employee = 0,
        Company = 1,
    }

    public enum PayrollType : byte
    {
        POT = 0,
        GP = 1,
        PPH = 2,
        TTT = 3,
        TT = 4,
        RAPEL = 5,
        PESANGON = 6,
        T3 = 7
    }

    public enum PaymentStatus : byte
    {
        After = 0,
        Before = 1
    }

    public enum BenefitStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum DeductionStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum SalaryStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3,
        Compute = 4,
        Complete = 5,
        Deleted = 6
    }

    public enum ScheduleShiftStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum EmpGroupAssignStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum SlipStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum SKSalaryStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum PayrollStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum PPhYearStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posted = 3
    }

    public enum AttendanceDailyStatus : byte
    {
        Automatic = 0,
        ManualChange = 1,
        Transfer = 2
    }

    public enum AttendanceClockStatus : byte
    {
        Automatic = 0,
        Manual = 1
    }

    public enum RadiusUpdateVoucherStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum RadiusUpdateVoucherExpiredTimeUnit : byte
    {
        Hour = 0,
        Day = 1,
        Month = 2
    }

    public enum ReprimandStatus : byte
    {
        Lisan = 0,
        Tertulis = 1
    }

    public enum EmpRewardStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum EmpReprimandStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum THRStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum AppraisalStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum SalaryPaymentStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum ChangeShiftStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum AppraisalResult : byte
    {
        E = 0,
        D = 1,
        C = 2,
        B = 3,
        A = 4
    }

    public enum SMSCategoryEnum : byte
    {
        Other = 0,
        InetReg = 1,
        Konf = 2
    }

    public enum POSDiscountStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum POSDiscountPaymentType : byte
    {
        All = 0,
        Kredit = 1,
        Debit = 2,
        Voucher = 3,
        Cash = 4
    }

    public enum POSPromoPaymentType : byte
    {
        All = 0,
        Kredit = 1,
        Debit = 2,
        Voucher = 3,
        Cash = 4
    }

    public enum POSPaymentType : byte
    {
        Cash = 0,
        Kredit = 1,
        Debit = 2,
        Voucher = 3
    }

    public enum AdjustDiffRateStatus : byte
    {
        OnHold = 0,
        Posting = 1
    }

    public enum POSTrInternetStatus : byte
    {
        SendToCashier = 0,
        Posted = 1,
        DeliveryOrder = 2
    }

    public enum POSTrPrintingStatus : byte
    {
        SendToCashier = 0,
        Posted = 1,
        DeliveryOrder = 2
    }

    public enum POSTrPhotocopyStatus : byte
    {
        SendToCashier = 0,
        Posted = 1,
        DeliveryOrder = 2
    }

    public enum POSTrGraphicStatus : byte
    {
        SendToCashier = 0,
        Posted = 1,
        DeliveryOrder = 2
    }

    public enum POSTrCafeStatus : byte
    {
        SendToCashier = 0,
        Posted = 1,
        DeliveryOrder = 2
    }

    public enum POSTrRetailStatus : byte
    {
        NewEntry = 0,
        OnHold = 1,
        SendToCashier = 2,
        Posted = 3,
        DeliveryOrder = 4
    }

    public enum POSTrDeliveryStatus : byte
    {
        NewDeliveryOrder = 0,
        DeliveryProcess = 1,
        DoneProcess = 2,
        AssignRider = 3,
        ReceiptDeliveryOrder = 4,
        CloseDeliveryOrder = 5
    }

    public enum POSMsInternetTableStatus : byte
    {
        Available = 0,
        Booking = 1,
        NotAvailable = 2
    }

    public enum POSDeliveryOrderStatus : byte
    {
        Open = 0,
        Process = 1,
        Done = 2,
        Delivering = 3,
        Deliver = 4,
        Close = 5,
        Cancel = 6,
        Paid = 7
    }

    public enum POSTrSettlementStatus : byte
    {
        OnHold = 1,
        Posted = 2,
        VOID = 3
    }

    public enum POSTrSettleType : byte
    {
        Paid = 0,
        DP = 1
    }

    public enum POSDiscountAmountType : byte
    {
        Amount = 0,
        Percentage = 1
    }

    public enum POSDiscountCalcType : byte
    {
        Item = 0,
        Subtotal = 1
    }

    public enum POSPromoAmountType : byte
    {
        Amount = 0,
        Percentage = 1
    }

    public enum POSTransType : byte
    {
        Retail = 0,
        Internet = 1,
        Ticketing = 2,
        Printing = 3,
        Photocopy = 4,
        Shipping = 5,
        Graphic = 6,
        EVoucher = 7,
        Cafe = 8
    }

    public enum LeaveType : byte
    {
        Add = 0,
        Temp = 1
    }

    public enum POSSettlementButtonType : byte
    {
        Cash = 0,
        CreditCard = 1,
        Debit = 2,
        Voucher = 3,
        SplitCash = 4
    }

    public enum ModeType : byte
    {
        Percentage = 0,
        Amount = 1,
        FixedAmount = 2
    }

    public enum POSDoneSettlementStatus : byte
    {
        NotYet = 1,
        Done = 2
    }

    public enum POSDeliveryStatus : byte
    {
        NotYetDelivered = 1,
        Delivered = 2
    }

    public enum CompConfigDataType : byte
    {
        String,
        Multiline,
        Decimal,
        Int,
        Enum,
        SQLQuery
    }

    public enum POSFloorType : byte
    {
        Internet,
        Cafe
    }

    public enum LateDispensationStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

    public enum POSTrShippingStatus : byte
    {
        SendToCashier = 0,
        Posted = 1,
        DeliveryOrder = 2
    }

    public enum POSPromoStatus : byte
    {
        OnHold = 0,
        WaitingForApproval = 1,
        Approved = 2,
        Posting = 3
    }

}