using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;

namespace BusinessRule.POSInterface
{
    public class BoxLayout : Base
    {
        private String _roomCode, _stringPosition, _backgroundImage;
        private Int16 _lebarPanelLayout, _tinggiPanelLayout,
        _posisiXPanelLayout, _posisiYPanelLayout, _gridSpace,
        _tinggiBox, _lebarBox, _boxCount;
        private String[] _posX, _posY;

        public List<POSMsBoxLayout> GetListBoxLayout() {
            List<POSMsBoxLayout> _result = new List<POSMsBoxLayout>();
            try
            {
                var _qry = (from _layoutList in this.db.POSMsBoxLayouts select _layoutList);
                if (_qry.Count() > 0)
                    foreach (var _row in _qry)
                        _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex; 
            }
            return _result;
        }

        private void ExtractData(String _prmRoomCode)
        {
            //POSMsBoxLayout _posMsBoxLayout = this.db.POSMsBoxLayouts.Single(a=>a.roomCode == _prmRoomCode );
            POSMsBoxLayout _posMsBoxLayout = this.db.POSMsBoxLayouts.FirstOrDefault(a => a.roomCode == _prmRoomCode);
            this._roomCode = _posMsBoxLayout.roomCode;
            this._lebarPanelLayout = Convert.ToInt16 ( _posMsBoxLayout.lebarPanelLayout );
            this._tinggiPanelLayout = Convert.ToInt16 ( _posMsBoxLayout.tinggiPanelLayout );
            this._posisiXPanelLayout = Convert.ToInt16(_posMsBoxLayout.posisiXPanelLayout );
            this._posisiYPanelLayout = Convert.ToInt16(_posMsBoxLayout.posisiYPanelLayout);
            this._gridSpace = Convert.ToInt16(_posMsBoxLayout.gridSpace);
            this._tinggiBox = Convert.ToInt16(_posMsBoxLayout.tinggiBox);
            this._lebarBox = Convert.ToInt16(_posMsBoxLayout.lebarBox);
            this._stringPosition = _posMsBoxLayout.position ;
            this._backgroundImage = _posMsBoxLayout.backgroundImage;
            if (this._stringPosition == "")
                this._boxCount = 0;
            else
            {
                String[] _positions = this._stringPosition.Split('|');
                this._boxCount = Convert.ToInt16(_positions.Length);
                this._posX = new String[_boxCount];
                this._posY = new String[_boxCount];
                for (int i = 0; i < _boxCount; i++)
                {
                    String[] _posXY = _positions[i].Split(',');
                    this._posX[i] = _posXY[0];
                    this._posY[i] = _posXY[1];
                }
            }
        }

        public BoxLayout() {}
        public BoxLayout(String _prmRoomCode) {
            ExtractData(_prmRoomCode);
        }
        public Boolean UpdatePositionRoom(String _prmRoomCode, String _newStringPosition)
        {
            try
            {
                POSMsBoxLayout _updateData = this.db.POSMsBoxLayouts.Single(a => a.roomCode == _prmRoomCode);
                _updateData.position = _newStringPosition;
                this.db.SubmitChanges();                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String RoomCode
        {
            get { return this._roomCode; }
        }
        public Int16 LebarPanelLayout
        {
            get { return this._lebarPanelLayout; }
            set { this._lebarPanelLayout = value; }
        }
        public Int16 TinggiPanelLayout
        {
            get { return this._tinggiPanelLayout; }
            set { this._tinggiPanelLayout = value; }
        }
        public Int16 PosisiXPanelLayout
        {
            get { return this._posisiXPanelLayout; }
            set { this._posisiXPanelLayout = value; }
        }
        public Int16 PosisiYPanelLayout
        {
            get { return this._posisiYPanelLayout; }
            set { this._posisiYPanelLayout = value; }
        }
        public Int16 GridSpace
        {
            get { return this._gridSpace; }
            set { this._gridSpace = value; }
        }
        public Int16 TinggiBox
        {
            get { return this._tinggiBox; }
            set { this._tinggiBox = value; }
        }
        public Int16 LebarBox
        {
            get { return this._lebarBox; }
            set { this._lebarBox = value; }
        }
        public Int16 BoxCount
        {
            get { return this._boxCount; }
            set { this._boxCount = value; }
        }
        public String[] PosX
        {
            get { return this._posX; }
        }
        public String[] PosY
        {
            get { return this._posY; }
        }
        public String StringPosition
        {
            get { return this._stringPosition; }
            set { this._stringPosition = value; }
        }
        public String BackgroundImage {
            get { return this._backgroundImage; }
            set { this._backgroundImage = value; }
        }
        

        public void DeleteLayout(String _prmRoomCode)
        {
            try
            {
                POSMsBoxLayout _deleteData = this.db.POSMsBoxLayouts.Single(a => a.roomCode == _prmRoomCode);
                this.db.POSMsBoxLayouts.DeleteOnSubmit(_deleteData);
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool NewLayout(String _prmNewRoomCode, String _prmHeight, String _prmWidth)
        {
            bool _result = false;
            try
            {
                if ( this.db.POSMsBoxLayouts.Where (a=>a.roomCode == _prmNewRoomCode ).Count () == 0 ) {
                    POSMsBoxLayout _newData = new POSMsBoxLayout();
                    _newData.roomCode = _prmNewRoomCode;
                    _newData.lebarPanelLayout = Convert.ToInt16(_prmWidth);
                    _newData.tinggiPanelLayout = Convert.ToInt16(_prmHeight);
                    _newData.posisiXPanelLayout = 5;
                    _newData.posisiYPanelLayout = 55;
                    _newData.gridSpace = 10;
                    _newData.tinggiBox = 28;
                    _newData.lebarBox = 38;
                    _newData.position = "";
                    _newData.backgroundImage = "";
                    this.db.POSMsBoxLayouts.InsertOnSubmit(_newData);
                    this.db.SubmitChanges();
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSMsBoxLayout GetSinglePOSMsBoxLayout(String _prmRoomCode)
        {
            POSMsBoxLayout _result = new POSMsBoxLayout();

            try
            {
                _result = this.db.POSMsBoxLayouts.Single(_temp => _temp.roomCode == _prmRoomCode);
            }
            catch (Exception ex)
            {   
                throw ex;
            }

            return _result;
        }

        public bool SavePOSMsBoxLayout(POSMsBoxLayout _prmPOSMsBoxLayout)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {   
                throw ex;
            }

            return _result;
        }

    }
}
