namespace ShoppingCart.Models
{
    public class ResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public ResponseModel(bool success, string message, object data = null)
        {
            this.success = success;
            this.message = message;
            this.data = data;
        }
        public ResponseModel()
        {

        }

        public ResponseModel ResponseSuccess()
        {
            return new ResponseModel(true
                , "บันทึกข้อมูลสำเร็จ"
                , "");
        }

        public ResponseModel ResponseSuccess(string message)
        {
            return new ResponseModel(true
                , message
                , "");
        }

        public ResponseModel ResponseSuccess(string message, object data)
        {
            return new ResponseModel(true
                , message
                , data);
        }

        public ResponseModel ResponseError()
        {
            return new ResponseModel(false
                , "บันทึกข้อมูลไม่สำเร็จ"
                , "");
        }

        public ResponseModel ResponseError(string message)
        {
            return new ResponseModel(false
                , message
                , "");
        }
    }
}
