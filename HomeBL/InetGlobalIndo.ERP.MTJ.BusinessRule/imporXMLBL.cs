using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public class imporXMLBL : Base
    {
        public imporXMLBL() { }

        public void importXMLtoCafe()
        {
            this.db.sp_NCCImporXML();
            XDocument input = XDocument.Load(ApplicationConfig.UploadXMLPath + "xmlfile.xml");

            var _qryRepairId = (
                    from _ncc_cafeHd in this.db.NCC_CafeHds
                    select _ncc_cafeHd.RepairId
                ).Distinct();

            foreach (String _repairId in _qryRepairId)
            {
                if ((from _nccAdditionalSymptom in this.db.NCC_CafeAdditionalSymptomInterBCForExports
                     where _nccAdditionalSymptom.RepairId == _repairId
                     select _nccAdditionalSymptom).Count() == 0)
                {
                    #region ICONAdditionalSymptomInterBCForExport
                    var _qryICONAdditionalSymptomInterBCForExport =
                            (
                            from key in input.Root.Elements("PropertySet").Elements("PropertySet").Elements("SiebelMessage").Elements("ListOfICONXMLExportIO").Elements("ICONServiceRequestBCforExport")
                            where key.Attribute("RepairId").Value.ToString() == _repairId
                            select key.Elements("ListOfICONAdditionalSymptomInterBCForExport").Elements("ICONAdditionalSymptomInterBCForExport")
                            );
                    int _ctrICONAdditionalSymptomInterBCForExport = 1;
                    foreach (var _rs in _qryICONAdditionalSymptomInterBCForExport)
                    {
                        String _name = "";

                        foreach (var _resultValue in _rs.Attributes("Name"))
                        {
                            _name = _resultValue.Value.ToString();
                        }
                        NCC_CafeAdditionalSymptomInterBCForExport _addData = new NCC_CafeAdditionalSymptomInterBCForExport();
                        _addData.RepairId = _repairId;
                        _addData.RepairIdItem = _ctrICONAdditionalSymptomInterBCForExport++;
                        _addData.Name = _name;
                        this.db.NCC_CafeAdditionalSymptomInterBCForExports.InsertOnSubmit(_addData);
                    }
                    #endregion
                }

                if ((from _nccAgreement in this.db.NCC_CafeAssetBCForExports
                     where _nccAgreement.RepairId == _repairId
                     select _nccAgreement).Count() == 0)
                {
                    #region ICONAgreementEntitlementBCForExport
                    var _qryICONAgreementEntitlementBCForExport =
                            (
                            from key in input.Root.Elements("PropertySet").Elements("PropertySet").Elements("SiebelMessage").Elements("ListOfICONXMLExportIO").Elements("ICONServiceRequestBCforExport")
                            where key.Attribute("RepairId").Value.ToString() == _repairId
                            select key.Elements("ListOfICONAssetMgmt-AssetBCForExport").Elements("ICONAssetMgmt-AssetBCForExport").Elements("ListOfICONAgreementEntitlementBCForExport").Elements("ICONAgreementEntitlementBCForExport")
                            );
                    int _ctrICONAgreementEntitlementBCForExport = 1;
                    foreach (var _rs in _qryICONAgreementEntitlementBCForExport)
                    {
                        String _careServiceLanguage = "";
                        String _entitlementId = "";
                        String _name = "";
                        String _description = "";

                        foreach (var _resultValue in _rs.Attributes("CareServiceLanguage"))
                        {
                            _careServiceLanguage = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("EntitlementId"))
                        {
                            _entitlementId = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("Name"))
                        {
                            _name = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("Description"))
                        {
                            _description = _resultValue.Value.ToString();
                        }
                        NCC_CafeAssetBCForExport _addData = new NCC_CafeAssetBCForExport();
                        _addData.RepairId = _repairId;
                        _addData.RepairIdItem = _ctrICONAgreementEntitlementBCForExport++;
                        _addData.CareServiceLanguage = _careServiceLanguage;
                        _addData.EntitlementId = _entitlementId;
                        _addData.Name = _name;
                        _addData.Description = _description;
                        this.db.NCC_CafeAssetBCForExports.InsertOnSubmit(_addData);
                    }
                    #endregion
                }

                if ((from _nccAttach in this.db.NCC_CafeAttachedMEBCforExports
                     where _nccAttach.RepairId == _repairId
                     select _nccAttach).Count() == 0)
                {
                    #region ICONAttachedMEBCforExport
                    var _qryICONAttachedMEBCforExport =
                            (
                            from key in input.Root.Elements("PropertySet").Elements("PropertySet").Elements("SiebelMessage").Elements("ListOfICONXMLExportIO").Elements("ICONServiceRequestBCforExport")
                            where key.Attribute("RepairId").Value.ToString() == _repairId
                            select key.Elements("ListOfICONAttachedMEBCforExport").Elements("ICONAttachedMEBCforExport")
                            );
                    int _ctrICONAttachedMEBCforExport = 1;
                    foreach (var _rs in _qryICONAttachedMEBCforExport)
                    {
                        String _attachedMEs = "";

                        foreach (var _resultValue in _rs.Attributes("AttachedMEs"))
                        {
                            _attachedMEs = _resultValue.Value.ToString();
                        }
                        NCC_CafeAttachedMEBCforExport _addData = new NCC_CafeAttachedMEBCforExport();
                        _addData.RepairId = _repairId;
                        _addData.RepairIdItem = _ctrICONAttachedMEBCforExport++;
                        _addData.AttachedMEs = _attachedMEs;
                        this.db.NCC_CafeAttachedMEBCforExports.InsertOnSubmit(_addData);
                    }
                    #endregion
                }

                if ((from _nccAuditTrail in this.db.NCC_CafeAuditTrailItemforExports
                     where _nccAuditTrail.RepairId == _repairId
                     select _nccAuditTrail).Count() == 0)
                {
                    #region ICONAuditTrailItemforExport
                    var _qryICONAuditTrailItemforExport =
                            (
                        //from key in input.Root.Elements("PropertySet").Elements("PropertySet").Elements("SiebelMessage").Elements("ListOfICONXMLExportIO").Elements("ICONServiceRequestBCforExport").Elements("ListOfICONAuditTrailItemforExport").Elements("ICONAuditTrailItemforExport").Attributes("EmployeeLogin")
                            from key in input.Root.Elements("PropertySet").Elements("PropertySet").Elements("SiebelMessage").Elements("ListOfICONXMLExportIO").Elements("ICONServiceRequestBCforExport")
                            //.Elements("ListOfICONAuditTrailItemforExport").Elements("ICONAuditTrailItemforExport").Attributes("EmployeeLogin")
                            where key.Attribute("RepairId").Value.ToString() == _repairId
                            //SiebelMessage
                            //    PropertySet
                            //            PropertySet
                            //                SiebelMessage
                            //            ListOfICONXMLExportIO
                            //                ICONServiceRequestBCforExport
                            //                //////////////////////////////
                            //                    ListOfICONAuditTrailItemforExport
                            //                        ICONAuditTrailItemforExport

                            //select key
                            select key.Elements("ListOfICONAuditTrailItemforExport").Elements("ICONAuditTrailItemforExport")
                            );
                    int _ctrICONAuditTrailItemforExport = 1;
                    foreach (var _rs in _qryICONAuditTrailItemforExport)
                    {
                        String _employeeLogin = "";
                        String _newValue = "";
                        String _date = "";
                        String _field = "";
                        String _oldValue = "";
                        String _operation = "";

                        foreach (var _resultValue in _rs.Attributes("EmployeeLogin"))
                        {
                            _employeeLogin = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("NewValue"))
                        {
                            _newValue = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("Date"))
                        {
                            _date = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("Field"))
                        {
                            _field = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("OldValue"))
                        {
                            _oldValue = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("Operation"))
                        {
                            _operation = _resultValue.Value.ToString();
                        }
                        NCC_CafeAuditTrailItemforExport _addData = new NCC_CafeAuditTrailItemforExport();
                        _addData.RepairId = _repairId;
                        _addData.RepairIdItem = _ctrICONAuditTrailItemforExport++;
                        _addData.EmployeeLogin = _employeeLogin;
                        _addData.NewValue = _newValue;
                        _addData.Date = _date;
                        _addData.Field = _field;
                        _addData.OldValue = _oldValue;
                        _addData.Operation = _operation;
                        this.db.NCC_CafeAuditTrailItemforExports.InsertOnSubmit(_addData);
                    }
                    #endregion
                }

                if ((from _nccWO in this.db.NCC_CafeWorkOrderBCforExports
                     where _nccWO.RepairId == _repairId
                     select _nccWO).Count() == 0)
                {
                    #region ICONWorkOrderBCforExport
                    var _qryICONWorkOrderBCforExport =
                            (
                            from key in input.Root.Elements("PropertySet").Elements("PropertySet").Elements("SiebelMessage").Elements("ListOfICONXMLExportIO").Elements("ICONServiceRequestBCforExport")
                            where key.Attribute("RepairId").Value.ToString() == _repairId
                            select key.Elements("ListOfICONWorkOrderBCforExport").Elements("ICONWorkOrderBCforExport")
                            );
                    int _ctrICONWorkOrderBCforExport = 1;
                    foreach (var _rs in _qryICONWorkOrderBCforExport)
                    {
                        String _ServiceCode = "";
                        String _SWAPDateTime = "";
                        String _WorkOrderStatus = "";
                        String _SWAPSerialNumber = "";
                        String _NewSWVersion = "";

                        String _SWAPReasonCode = "";
                        String _SWAPProductCode = "";
                        String _OrganizationId = "";
                        String _CreatedByName = "";

                        String _ASVCode = "";
                        String _TransferredByNMS = "";
                        String _ServiceLevel = "";
                        String _WOandClaimID = "";
                        String _WorkOrderSubStatus = "";

                        String _SWVersion = "";
                        String _TechnicianComments = "";
                        String _SWID = "";
                        String _DeviceIdentified = "";
                        String _SWAPAdditionalSerialNumber = "";

                        String _Created = "";
                        String _ReceivedbyNMS = "";
                        String _TransferTo = "";
                        String _Createdin = "";
                        String _SWAPModel = "";

                        foreach (var _resultValue in _rs.Attributes("ServiceCode"))
                        {
                            _ServiceCode = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("SWAPDateTime"))
                        {
                            _SWAPDateTime = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("WorkOrderStatus"))
                        {
                            _WorkOrderStatus = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("SWAPSerialNumber"))
                        {
                            _SWAPSerialNumber = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("NewSWVersion"))
                        {
                            _NewSWVersion = _resultValue.Value.ToString();
                        }

                        foreach (var _resultValue in _rs.Attributes("SWAPReasonCode"))
                        {
                            _SWAPReasonCode = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("SWAPProductCode"))
                        {
                            _SWAPProductCode = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("OrganizationId"))
                        {
                            _OrganizationId = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("CreatedByName"))
                        {
                            _CreatedByName = _resultValue.Value.ToString();
                        }


                        foreach (var _resultValue in _rs.Attributes("ASVCode"))
                        {
                            _ASVCode = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("TransferredByNMS"))
                        {
                            _TransferredByNMS = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("ServiceLevel"))
                        {
                            _ServiceLevel = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("WOandClaimID"))
                        {
                            _WOandClaimID = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("WorkOrderSubStatus"))
                        {
                            _WorkOrderSubStatus = _resultValue.Value.ToString();
                        }


                        foreach (var _resultValue in _rs.Attributes("SWVersion"))
                        {
                            _SWVersion = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("TechnicianComments"))
                        {
                            _TechnicianComments = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("SWID"))
                        {
                            _SWID = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("DeviceIdentified"))
                        {
                            _DeviceIdentified = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("SWAPAdditionalSerialNumber"))
                        {
                            _SWAPAdditionalSerialNumber = _resultValue.Value.ToString();
                        }


                        foreach (var _resultValue in _rs.Attributes("Created"))
                        {
                            _Created = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("ReceivedbyNMS"))
                        {
                            _ReceivedbyNMS = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("TransferTo"))
                        {
                            _TransferTo = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("Createdin"))
                        {
                            _Createdin = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("SWAPModel"))
                        {
                            _SWAPModel = _resultValue.Value.ToString();
                        }
                        NCC_CafeWorkOrderBCforExport _addData = new NCC_CafeWorkOrderBCforExport();
                        _addData.RepairId = _repairId;
                        _addData.RepairIdItem = _ctrICONWorkOrderBCforExport++;

                        _addData.ServiceCode = _ServiceCode;
                        _addData.SWAPDateTime = _SWAPDateTime;
                        _addData.WorkOrderStatus = _WorkOrderStatus;
                        _addData.SWAPSerialNumber = _SWAPSerialNumber;

                        _addData.NewSWVersion = _NewSWVersion;
                        _addData.SWAPReasonCode = _SWAPReasonCode;
                        _addData.SWAPProductCode = _SWAPProductCode;
                        _addData.OrganizationId = _OrganizationId;
                        _addData.CreatedByName = _CreatedByName;

                        _addData.ASVCode = _ASVCode;
                        _addData.TransferredByNMS = _TransferredByNMS;
                        _addData.ServiceLevel = _ServiceLevel;
                        _addData.WOandClaimID = _WOandClaimID;
                        _addData.WorkOrderSubStatus = _WorkOrderSubStatus;

                        _addData.SWVersion = _SWVersion;
                        _addData.TechnicianComments = _TechnicianComments;
                        _addData.SWID = _SWID;
                        _addData.DeviceIdentified = _DeviceIdentified;
                        _addData.SWAPAdditionalSerialNumber = _SWAPAdditionalSerialNumber;

                        _addData.Created = _Created;
                        _addData.ReceivedbyNMS = _ReceivedbyNMS;
                        _addData.TransferTo = _TransferTo;
                        _addData.Createdin = _Createdin;
                        _addData.SWAPModel = _SWAPModel;
                        this.db.NCC_CafeWorkOrderBCforExports.InsertOnSubmit(_addData);
                    }
                    #endregion

                    #region ICONWorkOrderDetailsBCforExport
                    var _qryICONWorkOrderDetailsBCforExport =
                            (
                            from key in input.Root.Elements("PropertySet").Elements("PropertySet").Elements("SiebelMessage").Elements("ListOfICONXMLExportIO").Elements("ICONServiceRequestBCforExport")
                            where key.Attribute("RepairId").Value.ToString() == _repairId
                            select key.Elements("ListOfICONWorkOrderBCforExport").Elements("ICONWorkOrderBCforExport").Elements("ListOfICONWorkOrderDetailsBCforExport").Elements("ICONWorkOrderDetailsBCforExport")
                            );
                    int _ctrICONWorkOrderDetailsBCforExport = 1;
                    foreach (var _rs in _qryICONWorkOrderDetailsBCforExport)
                    {
                        String _DealerCreatedIn = "";
                        String _CCTReferenceNo = "";
                        String _SparePartNumber = "";
                        String _CreatedBy = "";
                        String _FaultCode = "";
                        String _RepairSymptomCode = "";
                        String _Type = "";
                        String _SparePartDescription = "";
                        String _KeyRepair = "";
                        String _PartReplaced = "";
                        String _Created = "";
                        String _FieldServiceBulletin = "";

                        foreach (var _resultValue in _rs.Attributes("DealerCreatedIn"))
                        {
                            _DealerCreatedIn = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("CCTReferenceNo"))
                        {
                            _CCTReferenceNo = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("SparePartNumber"))
                        {
                            _SparePartNumber = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("CreatedBy"))
                        {
                            _CreatedBy = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("FaultCode"))
                        {
                            _FaultCode = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("RepairSymptomCode"))
                        {
                            _RepairSymptomCode = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("Type"))
                        {
                            _Type = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("SparePartDescription"))
                        {
                            _SparePartDescription = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("KeyRepair"))
                        {
                            _KeyRepair = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("PartReplaced"))
                        {
                            _PartReplaced = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("Created"))
                        {
                            _Created = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("FieldServiceBulletin"))
                        {
                            _FieldServiceBulletin = _resultValue.Value.ToString();
                        }
                        NCC_CafeWorkOrderDetailsBCforExport _addData = new NCC_CafeWorkOrderDetailsBCforExport();
                        _addData.RepairId = _repairId;
                        _addData.RepairIdItem = _ctrICONWorkOrderDetailsBCforExport++;
                        _addData.DealerCreatedIn = _DealerCreatedIn;
                        _addData.CCTReferenceNo = _CCTReferenceNo;
                        _addData.SparePartNumber = _SparePartNumber;
                        _addData.CreatedBy = _CreatedBy;
                        _addData.FaultCode = _FaultCode;
                        _addData.RepairSymptomCode = _RepairSymptomCode;
                        _addData.Type = _Type;
                        _addData.SparePartDescription = _SparePartDescription;
                        _addData.KeyRepair = _KeyRepair;
                        _addData.PartReplaced = _PartReplaced;
                        _addData.Created = _Created;
                        _addData.FieldServiceBulletin = _FieldServiceBulletin;
                        this.db.NCC_CafeWorkOrderDetailsBCforExports.InsertOnSubmit(_addData);
                    }
                    #endregion

                    #region ICONWOTransferOrganizationBCForExport
                    var _qryICONWOTransferOrganizationBCForExport =
                            (
                            from key in input.Root.Elements("PropertySet").Elements("PropertySet").Elements("SiebelMessage").Elements("ListOfICONXMLExportIO").Elements("ICONServiceRequestBCforExport")
                            where key.Attribute("RepairId").Value.ToString() == _repairId
                            select key.Elements("ListOfICONWorkOrderBCforExport").Elements("ICONWorkOrderBCforExport").Elements("ListOfICONWOTransferOrganizationBCForExport").Elements("ICONWOTransferOrganizationBCForExport")
                            );
                    int _ctrICONWOTransferOrganizationBCForExport = 1;
                    foreach (var _rs in _qryICONWOTransferOrganizationBCForExport)
                    {
                        String _PostalCode = "";
                        String _PhoneNumber = "";
                        String _ASVName = "";
                        String _City = "";
                        String _State = "";
                        String _Country = "";
                        String _StreetAddress = "";
                        String _FaxNumber = "";

                        foreach (var _resultValue in _rs.Attributes("PostalCode"))
                        {
                            _PostalCode = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("PhoneNumber"))
                        {
                            _PhoneNumber = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("ASVName"))
                        {
                            _ASVName = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("City"))
                        {
                            _City = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("State"))
                        {
                            _State = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("Country"))
                        {
                            _Country = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("StreetAddress"))
                        {
                            _StreetAddress = _resultValue.Value.ToString();
                        }
                        foreach (var _resultValue in _rs.Attributes("FaxNumber"))
                        {
                            _FaxNumber = _resultValue.Value.ToString();
                        }
                        NCC_CafeWOTransferOrganizationBCForExport _addData = new NCC_CafeWOTransferOrganizationBCForExport();
                        _addData.RepairId = _repairId;
                        _addData.RepairIdItem = _ctrICONWOTransferOrganizationBCForExport++;
                        _addData.PostalCode = _PostalCode;
                        _addData.PhoneNumber = _PhoneNumber;
                        _addData.ASVName = _ASVName;
                        _addData.City = _City;
                        _addData.State = _State;
                        _addData.Country = _Country;
                        _addData.StreetAddress = _StreetAddress;
                        _addData.FaxNumber = _FaxNumber;
                        this.db.NCC_CafeWOTransferOrganizationBCForExports.InsertOnSubmit(_addData);
                    }
                    #endregion
                }

                this.db.SubmitChanges();
            }


        }

        ~imporXMLBL() { }
    }
}
