/*
 Navicat Premium Data Transfer

 Source Server         : postgre
 Source Server Type    : PostgreSQL
 Source Server Version : 140004
 Source Host           : localhost:5432
 Source Catalog        : KAS.ECOS
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 140004
 File Encoding         : 65001

 Date: 12/10/2022 11:51:26
*/


-- ----------------------------
-- Sequence structure for Devies_ID_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."Devies_ID_seq";
CREATE SEQUENCE "public"."Devies_ID_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for Tokens.Log_STT_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."Tokens.Log_STT_seq";
CREATE SEQUENCE "public"."Tokens.Log_STT_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 9223372036854775807
START 1
CACHE 1;

-- ----------------------------
-- Table structure for Customers
-- ----------------------------
DROP TABLE IF EXISTS "public"."Customers";
CREATE TABLE "public"."Customers" (
  "ID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "ParentID" varchar(50) COLLATE "pg_catalog"."default",
  "Name" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "CreatedDate" timestamptz(6) NOT NULL DEFAULT now(),
  "isDeleted" bool NOT NULL DEFAULT false,
  "Address" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "HandPhone" varchar(50) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ''::character varying,
  "Email" varchar(50) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ''::character varying,
  "Description" varchar(255) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ''::character varying,
  "CustomerCode_Smac" varchar(50) COLLATE "pg_catalog"."default" NOT NULL
)
;
COMMENT ON COLUMN "public"."Customers"."CreatedDate" IS 'Ngày tạo';
COMMENT ON COLUMN "public"."Customers"."isDeleted" IS 'Mặc định là false';
COMMENT ON COLUMN "public"."Customers"."Address" IS 'Địa chỉ';
COMMENT ON COLUMN "public"."Customers"."HandPhone" IS 'Số điện thoại khách hàng';
COMMENT ON COLUMN "public"."Customers"."Description" IS 'Ghi chú';
COMMENT ON COLUMN "public"."Customers"."CustomerCode_Smac" IS 'mã khách hàng trên SMAC';
COMMENT ON TABLE "public"."Customers" IS 'Quản lý danh sách khách hàng';

-- ----------------------------
-- Records of Customers
-- ----------------------------
INSERT INTO "public"."Customers" VALUES ('ACHAUCONSULTING', '', 'Á CHÂU CONSULTING', '2022-09-15 11:35:34.07055+07', 'f', '', '', '', '', 'ACHAUCONSULTING');

-- ----------------------------
-- Table structure for Customers.Content.Permissions
-- ----------------------------
DROP TABLE IF EXISTS "public"."Customers.Content.Permissions";
CREATE TABLE "public"."Customers.Content.Permissions" (
  "CustomerID.Source" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "RoleName" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "isAllow" bool NOT NULL DEFAULT false,
  "CustomerID.Destination" varchar[] COLLATE "pg_catalog"."default" NOT NULL
)
;
COMMENT ON COLUMN "public"."Customers.Content.Permissions"."CustomerID.Source" IS 'Mã khách hàng / công ty/ Phòng ban xem dữ liệu của CustomerID.Destination';
COMMENT ON COLUMN "public"."Customers.Content.Permissions"."CustomerID.Destination" IS 'Mã phòng ban được xem dữ liệu bởi CustomerID.Source';
COMMENT ON TABLE "public"."Customers.Content.Permissions" IS 'Phân quyền nội dung';

-- ----------------------------
-- Records of Customers.Content.Permissions
-- ----------------------------

-- ----------------------------
-- Table structure for Customers.Info
-- ----------------------------
DROP TABLE IF EXISTS "public"."Customers.Info";
CREATE TABLE "public"."Customers.Info" (
  "ID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "Anything" varchar(50) COLLATE "pg_catalog"."default" NOT NULL
)
;
COMMENT ON COLUMN "public"."Customers.Info"."Anything" IS 'Điện thoại cầm tay';
COMMENT ON TABLE "public"."Customers.Info" IS 'Table này lưu trữ các thông tin mở rộng của Customers';

-- ----------------------------
-- Records of Customers.Info
-- ----------------------------

-- ----------------------------
-- Table structure for Customers.Profiles
-- ----------------------------
DROP TABLE IF EXISTS "public"."Customers.Profiles";
CREATE TABLE "public"."Customers.Profiles" (
  "CustomerID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "ProductID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "ProfileAPI" json NOT NULL,
  "isDelete" bool NOT NULL DEFAULT false,
  "ProfileDB" json NOT NULL
)
;
COMMENT ON COLUMN "public"."Customers.Profiles"."ProductID" IS 'Tên sản phẩm của';
COMMENT ON COLUMN "public"."Customers.Profiles"."ProfileAPI" IS 'Lưu thông tin Server của API, phục vụ cho FrontEnd';
COMMENT ON COLUMN "public"."Customers.Profiles"."isDelete" IS 'Mặc định là False';
COMMENT ON COLUMN "public"."Customers.Profiles"."ProfileDB" IS 'Lưu trữ thông tin DB (M1,M2,P1,P2)';
COMMENT ON TABLE "public"."Customers.Profiles" IS 'Danh sách thông tin cấu hình của Customers.';

-- ----------------------------
-- Records of Customers.Profiles
-- ----------------------------
INSERT INTO "public"."Customers.Profiles" VALUES ('ACHAUCONSULTING', 'SMAC', '[]', 'f', '[
  {
    "IP": "localhost",
    "typeProfile": 11,
    "username": "",
    "password": "Xsdcabudvhs=",
    "passwordDecrypt": "",
    "Port": 27017,
    "Name": "smac-achauconsulting"
  },
  {
    "IP": "localhost",
    "typeProfile": 11,
    "username": "",
    "password": "Xsdcabudvhs=",
    "passwordDecrypt": "",
    "Port": 27017,
    "Name": "warehouse-achauconsulting"
  },
  {
    "IP": "localhost",
    "typeProfile": 11,
    "username": "",
    "password": "Xsdcabudvhs=",
    "passwordDecrypt": "",
    "Port": 27017,
    "Name": "zLog-smac-achauconsulting"
  },
  {
    "IP": "localhost",
    "typeProfile": 11,
    "username": "",
    "password": "Xsdcabudvhs=",
    "passwordDecrypt": "",
    "Port": 27017,
    "Name": "zLog-warehouse-achauconsulting"
  }
]');

-- ----------------------------
-- Table structure for Devices
-- ----------------------------
DROP TABLE IF EXISTS "public"."Devices";
CREATE TABLE "public"."Devices" (
  "DeviceID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CustomerID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "ProductsID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateDate" timestamptz(6) NOT NULL DEFAULT now(),
  "DeviceName" varchar(200) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ''::character varying,
  "IP" varchar(50) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ''::character varying,
  "AccessDate" timestamptz(6) NOT NULL DEFAULT now(),
  "CustomerIDRoot" varchar(50) COLLATE "pg_catalog"."default" NOT NULL DEFAULT ''::character varying,
  "ID" int8 NOT NULL DEFAULT nextval('"Devies_ID_seq"'::regclass),
  "Location" json,
  "OSName" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "OSVer" varchar(50) COLLATE "pg_catalog"."default" NOT NULL
)
;
COMMENT ON COLUMN "public"."Devices"."DeviceID" IS 'Mã duy nhất của thiết bị. Window sẽ dùng thuật toán riêng, các hệ điều hành khác sẽ dùng function lấy mã thiết bị duy nhất';
COMMENT ON COLUMN "public"."Devices"."CustomerID" IS 'Mã khách hàng';
COMMENT ON COLUMN "public"."Devices"."ProductsID" IS 'Mã sản phẩm của KAS';
COMMENT ON COLUMN "public"."Devices"."CreateDate" IS 'Ngày kích hoạt.Hệ thống tự tạo, không cần gán giá trị. ';
COMMENT ON COLUMN "public"."Devices"."DeviceName" IS 'Tên thiết bị';
COMMENT ON COLUMN "public"."Devices"."IP" IS 'IP truy cập lần cuối';
COMMENT ON COLUMN "public"."Devices"."AccessDate" IS 'Thời gian truy cập lần cuối';
COMMENT ON COLUMN "public"."Devices"."CustomerIDRoot" IS 'Mã khách hàng có ParantID là null';
COMMENT ON COLUMN "public"."Devices"."ID" IS 'Mã tăng tự động';
COMMENT ON COLUMN "public"."Devices"."Location" IS 'Lưu trữ thông tin địa điểm truy cập';
COMMENT ON COLUMN "public"."Devices"."OSName" IS 'Tên hệ điều hành';
COMMENT ON COLUMN "public"."Devices"."OSVer" IS 'Phiên bản hệ điều hành';

-- ----------------------------
-- Records of Devices
-- ----------------------------

-- ----------------------------
-- Table structure for ECOS.User
-- ----------------------------
DROP TABLE IF EXISTS "public"."ECOS.User";
CREATE TABLE "public"."ECOS.User" (
  "PhoneNumber" varchar(20) COLLATE "pg_catalog"."default" NOT NULL,
  "FullName" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "LastAccesDate" timestamptz(6) NOT NULL DEFAULT now(),
  "IP" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "Description" varchar(255) COLLATE "pg_catalog"."default",
  "isDeleted" bool NOT NULL DEFAULT false
)
;
COMMENT ON COLUMN "public"."ECOS.User"."PhoneNumber" IS 'Số điện thoại được quyền login vào ECOS';
COMMENT ON COLUMN "public"."ECOS.User"."FullName" IS 'Tên người truy cập vào ECOS';
COMMENT ON COLUMN "public"."ECOS.User"."LastAccesDate" IS 'Lần cuối truy cập';
COMMENT ON COLUMN "public"."ECOS.User"."IP" IS 'IP truy cập';
COMMENT ON COLUMN "public"."ECOS.User"."Description" IS 'Thông tin bổ sung cần lưu ý';

-- ----------------------------
-- Records of ECOS.User
-- ----------------------------

-- ----------------------------
-- Table structure for Ping
-- ----------------------------
DROP TABLE IF EXISTS "public"."Ping";
CREATE TABLE "public"."Ping" (
  "KasProductName" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CustomerID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "SiteID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "PingTime" timestamptz(6) NOT NULL DEFAULT now(),
  "Version" varchar(50) COLLATE "pg_catalog"."default",
  "MachineInfo" varchar(255) COLLATE "pg_catalog"."default",
  "UserInfo" varchar(255) COLLATE "pg_catalog"."default",
  "UserId" varchar(50) COLLATE "pg_catalog"."default" NOT NULL
)
;
COMMENT ON COLUMN "public"."Ping"."KasProductName" IS 'Tên sản phẩm của KAS';
COMMENT ON COLUMN "public"."Ping"."CustomerID" IS 'Mã khách hàng';
COMMENT ON COLUMN "public"."Ping"."SiteID" IS 'Mã chi nhánh';
COMMENT ON COLUMN "public"."Ping"."PingTime" IS 'Lần cuối đăng nhập';
COMMENT ON COLUMN "public"."Ping"."Version" IS 'Phiên bản APP';
COMMENT ON COLUMN "public"."Ping"."MachineInfo" IS 'Thông tin thiết bị';
COMMENT ON COLUMN "public"."Ping"."UserInfo" IS 'Thông tin user';
COMMENT ON COLUMN "public"."Ping"."UserId" IS 'User đăng nhập';

-- ----------------------------
-- Records of Ping
-- ----------------------------

-- ----------------------------
-- Table structure for Products
-- ----------------------------
DROP TABLE IF EXISTS "public"."Products";
CREATE TABLE "public"."Products" (
  "ID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "Description" varchar(2000) COLLATE "pg_catalog"."default"
)
;
COMMENT ON COLUMN "public"."Products"."Description" IS 'Mô tả sản phẩm của KAS. Tối đa 2000 ký tự';
COMMENT ON TABLE "public"."Products" IS 'Các sản phẩm của KAS';

-- ----------------------------
-- Records of Products
-- ----------------------------
INSERT INTO "public"."Products" VALUES ('HOS', NULL);
INSERT INTO "public"."Products" VALUES ('MOS', '');
INSERT INTO "public"."Products" VALUES ('WEB', NULL);
INSERT INTO "public"."Products" VALUES ('POS', NULL);
INSERT INTO "public"."Products" VALUES ('CAP', NULL);
INSERT INTO "public"."Products" VALUES ('SMAC', NULL);

-- ----------------------------
-- Table structure for Products.Functions
-- ----------------------------
DROP TABLE IF EXISTS "public"."Products.Functions";
CREATE TABLE "public"."Products.Functions" (
  "Product.ID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "FunctionName" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "FunctionParent" varchar(50) COLLATE "pg_catalog"."default",
  "FunctionPath" varchar(2000) COLLATE "pg_catalog"."default",
  "Description" varchar(2000) COLLATE "pg_catalog"."default",
  "isDeleted" bool NOT NULL DEFAULT false,
  "Level" int2 NOT NULL DEFAULT 0
)
;
COMMENT ON COLUMN "public"."Products.Functions"."Level" IS 'Level của đệ quy Function';
COMMENT ON TABLE "public"."Products.Functions" IS 'Danh sách tính năng của sản phẩm KAS';

-- ----------------------------
-- Records of Products.Functions
-- ----------------------------

-- ----------------------------
-- Table structure for Products.Functions.Permissions
-- ----------------------------
DROP TABLE IF EXISTS "public"."Products.Functions.Permissions";
CREATE TABLE "public"."Products.Functions.Permissions" (
  "Product.ID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "FunctionName" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CustomerID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "RoleName" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "Permission" int2 NOT NULL DEFAULT 0,
  "MaxRecords" int4 NOT NULL DEFAULT 0,
  "isDeleted" bool NOT NULL DEFAULT false,
  "Expired" timestamptz(6)
)
;
COMMENT ON COLUMN "public"."Products.Functions.Permissions"."Permission" IS 'Phân quyền tính năng theo Enum(hệ số Bit)';
COMMENT ON COLUMN "public"."Products.Functions.Permissions"."MaxRecords" IS 'Mặc định là 0, không giới hạn. Field này dùng để giới hạn số lượng record cho các tính năng dùng thử miễn phí';
COMMENT ON COLUMN "public"."Products.Functions.Permissions"."Expired" IS 'Ngày hết hạn của Function, hạn sử dụng của Token sẽ bằng thời ngày hết hạn gần nhất của tất cả tính năng, và tối đa là 1 năm';
COMMENT ON TABLE "public"."Products.Functions.Permissions" IS 'Phân quyền tính năng.

Tại thời điểm đăng nhập:
	- Input: KAS.Product.ID , CustomerID,RoleName
	- Nếu giá trị trả về danh sách  FunctionName ==0, thì trả về thông báo tài khoản không hợp lệ. Dùng cho trường hợp 1 tài khoản truy cập vào nhiều sản phẩm';

-- ----------------------------
-- Records of Products.Functions.Permissions
-- ----------------------------

-- ----------------------------
-- Table structure for ROLES
-- ----------------------------
DROP TABLE IF EXISTS "public"."ROLES";
CREATE TABLE "public"."ROLES" (
  "CustomerID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "RoleName" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "isDeleted" bool DEFAULT false,
  "Description" varchar(2000) COLLATE "pg_catalog"."default",
  "CreateDate" timestamptz(6) DEFAULT now()
)
;
COMMENT ON COLUMN "public"."ROLES"."RoleName" IS 'Tên Vai trò';
COMMENT ON COLUMN "public"."ROLES"."Description" IS 'Mô tả vai trò';
COMMENT ON TABLE "public"."ROLES" IS 'Tạo ra các vai trò sử dụng tính năng (phân theo Customer), Nếu User được thêm vào Role này tương đương sẽ có các quyền tương tự ';

-- ----------------------------
-- Records of ROLES
-- ----------------------------

-- ----------------------------
-- Table structure for ROLES.Users
-- ----------------------------
DROP TABLE IF EXISTS "public"."ROLES.Users";
CREATE TABLE "public"."ROLES.Users" (
  "CustomerID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "RoleName" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "User" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "CreateDate" timestamptz(6) NOT NULL DEFAULT now(),
  "Password" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "Token" char(50) COLLATE "pg_catalog"."default" NOT NULL,
  "Token.Expired" timestamptz(6),
  "isDeleted" bool NOT NULL DEFAULT false,
  "ProductID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL
)
;
COMMENT ON COLUMN "public"."ROLES.Users"."User" IS 'Tại 1 thời điểm, chỉ có 1 Token gắn với 1 User. 
User và ProductID sẽ là duy nhất';
COMMENT ON COLUMN "public"."ROLES.Users"."Token" IS 'Token là duy nhất';
COMMENT ON COLUMN "public"."ROLES.Users"."ProductID" IS 'Tên sản phẩm của KAS. Khi login truyền vào Header';
COMMENT ON TABLE "public"."ROLES.Users" IS 'Quản lý tài khoản và token';

-- ----------------------------
-- Records of ROLES.Users
-- ----------------------------

-- ----------------------------
-- Table structure for SmacDPConfig
-- ----------------------------
DROP TABLE IF EXISTS "public"."SmacDPConfig";
CREATE TABLE "public"."SmacDPConfig" (
  "CustomerID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "ConfigKey" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "ConfigValue" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "isDeleted" bool NOT NULL,
  "DPClientID" varchar(50) COLLATE "pg_catalog"."default"
)
;
COMMENT ON COLUMN "public"."SmacDPConfig"."CustomerID" IS 'Mã khách hàng SMAC (CustomerCode)';
COMMENT ON COLUMN "public"."SmacDPConfig"."ConfigKey" IS 'Key cấu hình';
COMMENT ON COLUMN "public"."SmacDPConfig"."ConfigValue" IS 'Giá trị cấu hình';

-- ----------------------------
-- Records of SmacDPConfig
-- ----------------------------

-- ----------------------------
-- Table structure for SmacSession
-- ----------------------------
DROP TABLE IF EXISTS "public"."SmacSession";
CREATE TABLE "public"."SmacSession" (
  "CustomerID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "Session" varchar(30) COLLATE "pg_catalog"."default" NOT NULL,
  "Data" text COLLATE "pg_catalog"."default" NOT NULL,
  "LastUpdated" timestamptz(6) NOT NULL DEFAULT now()
)
;
COMMENT ON COLUMN "public"."SmacSession"."CustomerID" IS 'Mã Smac (CustomerCode)';
COMMENT ON COLUMN "public"."SmacSession"."Session" IS 'Session do smac cấp';
COMMENT ON COLUMN "public"."SmacSession"."Data" IS 'Thông tin đăng nhập';
COMMENT ON COLUMN "public"."SmacSession"."LastUpdated" IS 'Lần cuối cập nhật';

-- ----------------------------
-- Records of SmacSession
-- ----------------------------

-- ----------------------------
-- Table structure for Tokens.Log
-- ----------------------------
DROP TABLE IF EXISTS "public"."Tokens.Log";
CREATE TABLE "public"."Tokens.Log" (
  "CustomerID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "RoleName" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "User" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "DeviceID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "Token" varchar(2000) COLLATE "pg_catalog"."default" NOT NULL,
  "Token.Create" timetz(6) NOT NULL DEFAULT now(),
  "Token.Expired" timetz(6),
  "isDeleted" bool NOT NULL DEFAULT false,
  "KAS.Products.ID" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "STT" int8 NOT NULL DEFAULT nextval('"Tokens.Log_STT_seq"'::regclass)
)
;
COMMENT ON TABLE "public"."Tokens.Log" IS 'Quản lý cấp phát Token';

-- ----------------------------
-- Records of Tokens.Log
-- ----------------------------

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."Devies_ID_seq"
OWNED BY "public"."Devices"."ID";
SELECT setval('"public"."Devies_ID_seq"', 1, false);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."Tokens.Log_STT_seq"
OWNED BY "public"."Tokens.Log"."STT";
SELECT setval('"public"."Tokens.Log_STT_seq"', 1, false);

-- ----------------------------
-- Uniques structure for table Customers
-- ----------------------------
ALTER TABLE "public"."Customers" ADD CONSTRAINT "uCustomers_CustomerID" UNIQUE ("ID");

-- ----------------------------
-- Primary Key structure for table Customers
-- ----------------------------
ALTER TABLE "public"."Customers" ADD CONSTRAINT "Customers_pkey" PRIMARY KEY ("ID");

-- ----------------------------
-- Primary Key structure for table Customers.Content.Permissions
-- ----------------------------
ALTER TABLE "public"."Customers.Content.Permissions" ADD CONSTRAINT "Customers.Content.Permissions_pkey" PRIMARY KEY ("CustomerID.Source", "RoleName");

-- ----------------------------
-- Primary Key structure for table Customers.Info
-- ----------------------------
ALTER TABLE "public"."Customers.Info" ADD CONSTRAINT "CustomersInFo_pkey" PRIMARY KEY ("ID");

-- ----------------------------
-- Primary Key structure for table Customers.Profiles
-- ----------------------------
ALTER TABLE "public"."Customers.Profiles" ADD CONSTRAINT "Customers.Profiles_pkey" PRIMARY KEY ("CustomerID", "ProductID");

-- ----------------------------
-- Primary Key structure for table Devices
-- ----------------------------
ALTER TABLE "public"."Devices" ADD CONSTRAINT "Devies_pkey" PRIMARY KEY ("ID");

-- ----------------------------
-- Primary Key structure for table ECOS.User
-- ----------------------------
ALTER TABLE "public"."ECOS.User" ADD CONSTRAINT "ECOS.User_pkey" PRIMARY KEY ("PhoneNumber");

-- ----------------------------
-- Primary Key structure for table Ping
-- ----------------------------
ALTER TABLE "public"."Ping" ADD CONSTRAINT "Ping_pkey" PRIMARY KEY ("KasProductName", "CustomerID", "UserId", "SiteID");

-- ----------------------------
-- Primary Key structure for table Products
-- ----------------------------
ALTER TABLE "public"."Products" ADD CONSTRAINT "KAS.Products_pkey" PRIMARY KEY ("ID");

-- ----------------------------
-- Primary Key structure for table Products.Functions
-- ----------------------------
ALTER TABLE "public"."Products.Functions" ADD CONSTRAINT "KAS.Products.Functions_pkey" PRIMARY KEY ("Product.ID", "FunctionName");

-- ----------------------------
-- Primary Key structure for table Products.Functions.Permissions
-- ----------------------------
ALTER TABLE "public"."Products.Functions.Permissions" ADD CONSTRAINT "KAS.Products.Functions.Permissions_pkey" PRIMARY KEY ("Product.ID", "FunctionName", "CustomerID", "RoleName");

-- ----------------------------
-- Primary Key structure for table ROLES
-- ----------------------------
ALTER TABLE "public"."ROLES" ADD CONSTRAINT "ROLES_pkey" PRIMARY KEY ("CustomerID", "RoleName");

-- ----------------------------
-- Uniques structure for table ROLES.Users
-- ----------------------------
ALTER TABLE "public"."ROLES.Users" ADD CONSTRAINT "TokenUnique" UNIQUE ("Token");
ALTER TABLE "public"."ROLES.Users" ADD CONSTRAINT "usernameUnique" UNIQUE ("User", "ProductID");
COMMENT ON CONSTRAINT "TokenUnique" ON "public"."ROLES.Users" IS 'Token phải là duy nhất';
COMMENT ON CONSTRAINT "usernameUnique" ON "public"."ROLES.Users" IS 'User phải là duy nhất';

-- ----------------------------
-- Primary Key structure for table ROLES.Users
-- ----------------------------
ALTER TABLE "public"."ROLES.Users" ADD CONSTRAINT "ROLES.Users_pkey" PRIMARY KEY ("User", "ProductID", "CustomerID");

-- ----------------------------
-- Primary Key structure for table SmacDPConfig
-- ----------------------------
ALTER TABLE "public"."SmacDPConfig" ADD CONSTRAINT "SmacDPConfig_pkey" PRIMARY KEY ("CustomerID", "ConfigKey");

-- ----------------------------
-- Primary Key structure for table SmacSession
-- ----------------------------
ALTER TABLE "public"."SmacSession" ADD CONSTRAINT "SmacSession_pkey" PRIMARY KEY ("CustomerID", "Session");

-- ----------------------------
-- Primary Key structure for table Tokens.Log
-- ----------------------------
ALTER TABLE "public"."Tokens.Log" ADD CONSTRAINT "Tokens.Log_pkey" PRIMARY KEY ("STT");

-- ----------------------------
-- Foreign Keys structure for table Customers.Content.Permissions
-- ----------------------------
ALTER TABLE "public"."Customers.Content.Permissions" ADD CONSTRAINT "ROLES_Customers.Content.Permission_2K" FOREIGN KEY ("CustomerID.Source", "RoleName") REFERENCES "public"."ROLES" ("CustomerID", "RoleName") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table Customers.Info
-- ----------------------------
ALTER TABLE "public"."Customers.Info" ADD CONSTRAINT "Customers_CustomersInfo_ID" FOREIGN KEY ("ID") REFERENCES "public"."Customers" ("ID") ON DELETE NO ACTION ON UPDATE NO ACTION;

-- ----------------------------
-- Foreign Keys structure for table Customers.Profiles
-- ----------------------------
ALTER TABLE "public"."Customers.Profiles" ADD CONSTRAINT "Customers_Customers.Profiles_CustomerID" FOREIGN KEY ("CustomerID") REFERENCES "public"."Customers" ("ID") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."Customers.Profiles" ADD CONSTRAINT "Products_Customer.Profiles_ID" FOREIGN KEY ("ProductID") REFERENCES "public"."Products" ("ID") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table Products.Functions
-- ----------------------------
ALTER TABLE "public"."Products.Functions" ADD CONSTRAINT "Products_KAS.Products.Function_ID3" FOREIGN KEY ("Product.ID") REFERENCES "public"."Products" ("ID") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table Products.Functions.Permissions
-- ----------------------------
ALTER TABLE "public"."Products.Functions.Permissions" ADD CONSTRAINT "Products.Functions.Permissions_F1" FOREIGN KEY ("Product.ID", "FunctionName") REFERENCES "public"."Products.Functions" ("Product.ID", "FunctionName") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."Products.Functions.Permissions" ADD CONSTRAINT "ROLES.Products.Function.Permissions.FK2" FOREIGN KEY ("CustomerID", "RoleName") REFERENCES "public"."ROLES" ("CustomerID", "RoleName") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table ROLES
-- ----------------------------
ALTER TABLE "public"."ROLES" ADD CONSTRAINT "Customers_ROLES_CustomerID" FOREIGN KEY ("CustomerID") REFERENCES "public"."Customers" ("ID") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table ROLES.Users
-- ----------------------------
ALTER TABLE "public"."ROLES.Users" ADD CONSTRAINT "Product_RUS01" FOREIGN KEY ("ProductID") REFERENCES "public"."Products" ("ID") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."ROLES.Users" ADD CONSTRAINT "ROLES_ROLES.Users_K2" FOREIGN KEY ("CustomerID", "RoleName") REFERENCES "public"."ROLES" ("CustomerID", "RoleName") ON DELETE CASCADE ON UPDATE CASCADE;
