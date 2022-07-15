namespace KAS.API.MIDDEWARE.Entity
{
    public class InEntity
    {

        /// <summary>
        /// Đưa vào sign để kiểm tra, bắt buộc là GMT + 0
        /// </summary>
        public long clientTimeGMT { get; set; } = 0;




        /// <summary>
        /// Thông tin chi tiết thiết bị, chỉ dùng khi Đăng nhập hoặc Ping
        /// </summary>
        public object DeivceInfo { get; set; } = "";


        /// <summary>
        /// Khi chưa áp dụng KAS ECOS, giá trị này sẽ được add cứng
        /// </summary>
        public string Token { get; set; } = "";


        /// <summary>
        /// Phiên bản của Client, bắt buộc phải là 1 số, ví dụ POS là 1.0.0.2 sẽ chuyển thành 1002
        /// </summary>
        public int Version { get; set; } = 0;



        /// <summary>
        /// Chữ ký xác nhận đảm bảo dữ liệu không chỉnh sửa, không áp dụng với WEB
        /// </summary>
        public string Sign { get; set; } = "";


        /// <summary>
        /// Tên sản phẩm của KAS
        /// </summary>
        public string KASProductName { get; set; } = "";





        /// <summary>
        /// Thông tin địa điểm của request
        /// </summary>

        public object Location { get; set; } = "";


        /// <summary>
        /// IP của Client khi truy cập vào hệ thống
        /// </summary>
        public string IP { get; set; } = "";
        public bool isNeedEncrypt { get; set; } = false;
        public string data { get; set; } = "";
        public T getData<T>()
        {

            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            }
            catch
            {
                return default(T);
            }

        }

    }
}
