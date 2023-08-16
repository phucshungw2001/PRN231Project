USE [master]
GO
/****** Object:  Database [Warehouses]    Script Date: 8/16/2023 11:51:48 AM ******/
CREATE DATABASE [Warehouses]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Warehouses', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Warehouses.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Warehouses_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Warehouses_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Warehouses] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Warehouses].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Warehouses] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Warehouses] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Warehouses] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Warehouses] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Warehouses] SET ARITHABORT OFF 
GO
ALTER DATABASE [Warehouses] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Warehouses] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Warehouses] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Warehouses] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Warehouses] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Warehouses] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Warehouses] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Warehouses] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Warehouses] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Warehouses] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Warehouses] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Warehouses] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Warehouses] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Warehouses] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Warehouses] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Warehouses] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Warehouses] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Warehouses] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Warehouses] SET  MULTI_USER 
GO
ALTER DATABASE [Warehouses] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Warehouses] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Warehouses] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Warehouses] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Warehouses] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Warehouses] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Warehouses] SET QUERY_STORE = ON
GO
ALTER DATABASE [Warehouses] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Warehouses]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nchar](50) NULL,
	[Password] [nchar](50) NULL,
	[IsActive] [bit] NULL,
	[Role] [nchar](10) NULL,
	[ManagerID] [int] NULL,
	[CustomerID] [int] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](150) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](50) NULL,
	[Address] [nchar](10) NULL,
	[Phone] [nchar](10) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetails]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetails](
	[InvoiceDetailID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NULL,
	[Price] [float] NULL,
	[Discount] [float] NULL,
 CONSTRAINT [PK_InvoiceDetails] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[InvoicesID] [int] IDENTITY(1,1) NOT NULL,
	[InvoicesDate] [date] NULL,
	[CustomerID] [int] NULL,
	[InvoicesStatus] [bit] NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[InvoicesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Manager]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manager](
	[ManagerID] [int] IDENTITY(1,1) NOT NULL,
	[ManagerName] [nvarchar](150) NULL,
	[Address] [nvarchar](150) NULL,
	[Phone] [nvarchar](150) NULL,
 CONSTRAINT [PK_Manager] PRIMARY KEY CLUSTERED 
(
	[ManagerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prodcuts_Warehouses]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prodcuts_Warehouses](
	[ProductID] [int] NOT NULL,
	[WarehouseID] [int] NOT NULL,
 CONSTRAINT [PK_Prodcuts_Warehouses] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[WarehouseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](150) NULL,
	[Describe] [nvarchar](max) NULL,
	[Quantity] [int] NULL,
	[Price] [float] NULL,
	[WarehouseID] [int] NULL,
	[CategoryID] [int] NULL,
	[Status] [bit] NULL,
	[SuppliersID] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiptDetails]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiptDetails](
	[ReceiptDetailID] [int] NOT NULL,
	[ReceiptID] [int] NULL,
	[ProductID] [int] NULL,
	[EntryPrice] [float] NULL,
	[TotalValue] [float] NULL,
	[EntryUnit] [int] NULL,
 CONSTRAINT [PK_ReceiptDetails] PRIMARY KEY CLUSTERED 
(
	[ReceiptDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[AccountID] [int] NULL,
	[Roles] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Roles] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockReceipts]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockReceipts](
	[ReceiptID] [int] NOT NULL,
	[DateReceipt] [date] NULL,
	[WarehouseID] [int] NULL,
	[SupplierID] [int] NULL,
 CONSTRAINT [PK_StockReceipts] PRIMARY KEY CLUSTERED 
(
	[ReceiptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SuppliersID] [int] IDENTITY(1,1) NOT NULL,
	[SuppliersName] [nvarchar](150) NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nchar](10) NULL,
	[Email] [nchar](150) NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[SuppliersID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Warehouses]    Script Date: 8/16/2023 11:51:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouses](
	[WarehouseID] [int] IDENTITY(1,1) NOT NULL,
	[WarehouseName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
 CONSTRAINT [PK_Warehouses] PRIMARY KEY CLUSTERED 
(
	[WarehouseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountID], [UserName], [Password], [IsActive], [Role], [ManagerID], [CustomerID]) VALUES (1, N'customer01@gmail.com                              ', N'12345678                                          ', 1, N'USER      ', NULL, 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (1, N'Thức ăn cho mèo')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (2, N'Thức ăn cho chó')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (3, N'Dinh Dưỡng')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (4, N'Dụng cụ y tế ')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (5, N'Quần Áo')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (6, N'Đồ chơi')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (7, N'Phụ kiện ')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (8, N'Vệ sinh')
INSERT [dbo].[Category] ([CategoryID], [CategoryName]) VALUES (9, N'Chuồng và lồng')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerID], [CustomerName], [Address], [Phone]) VALUES (1, N'Nguyễn Phúc Hưng', N'Hà Nội    ', N'0987678975')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (1, N'Pate Lechat Exellence Mousse Rich', N'Pate Lechat Exellence Mousse Rich 85g là thức ăn nổi tiếng dạng mousse dành cho mèo từ Ý với những thành phần tươi ngon nhất, tuyệt hảo nhất.', 3000, 12000, 1, 1, 1, 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (2, N'Hạt Royal Canin Medium Puppy 4kg ', N'Những giống chó kích cỡ Medium trong khoảng 11 - 25kg được biết đến có nguồn năng lượng dồi dào vì đa số giống chó này được sử dụng hỗ trợ trong các công việc của con người từ rất lâu. Cho dù giống chó size Medium này được nuôi trong nhà hay dành thời gian hoạt động ngoài trời, với  thức ăn cho chó ROYAL CANIN Medium Puppy sẽ giúp chúng duy trì khả năng phòng thủ tự nhiên tốt hơn, đồng thời cung cấp năng lượng cân bằng cho giống chó này giúp duy trì trọng lượng khỏe mạnh', 3000, 19000, 1, 2, 1, 2)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (3, N'Viên uống hỗ trợ khử mùi giải độcNatural', N'Ba mẹ thường chỉ lo lắng khi thấy vật cưng của mình ăn uống kém, không đòi ăn. Chứ ít khi nghĩ vật cưng đang ăn, uống thức ăn không phù hợp với sinh lý và cũng để xảy ra nhiều lý do khác dẫn tới sự tiêu hóa kém, táo bón, mà như vậy đường ruột không được tẩy sạch và không loại thải hết độc tố. Các chất độc không bị loại thải ra khỏi cơ thể, tích tụ lâu ngày, chúng sẽ gây nên các bệnh mãn tính, dị ứng, lông da đặc biệt làm suy yếu các cơ quan gan - tụy, thận, tim mạch.', 800, 23000, 1, 3, 1, 3)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (4, N'Băng gạc y tế', N'Băng gạc y tế không dính, được sử dụng để băng bó vết thương và vết cắt nhỏ.', 1000, 8000, 1, 4, 1, 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (5, N'Áo hai dây gấu và que kem', N' khi shop ghi chất liệu là NỈ / LÔNG/ NHUNG / .... thì các chất vải này ÍT CO GIÃN nên khi bé của bạn bắp tay to / bụng béo / lưng dài / chân lùn ngắn/....thì nên cân nhắc lên thêm 1 size hoặc chỉ nên mua các loại vải thun để mặc

𝒔𝒐̂́ đ𝒐:  theo thứ tự size áo / vòng ngực/ chiều dài / số cân ước lượng
XS 30 / 20 / 500gr - 1.5kg
S 35 / 25 / 1.6 - 2.5kg
M 40 / 30/ 2.6kg - 4kg
L 45 / 35 / 4kg - 5.5kg
XL 50 / 38 / 5.6kg - 7.5kg
XXL 55 / 42 / 7.6kg - 10kg', 200, 15000, 1, 5, 1, 2)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (6, N'Đồ chơi cao su hình ngôi sao cho chó', N'Việc cho chó chơi đồ chơi là một giải pháp tuyệt vời giúp cún cưng của bạn trở nên nhanh nhẹn, hoạt bát hơn. Không những thế nó còn giúp cún cưng hạn chế cắn phá các vật dụng trong nhà. Chúng sẽ trở nên ngoan ngoãn và vâng lời hơn. Nếu bạn đang băn khoăn chưa biết lựa chọn loại đồ chơi nào phù hợp trong vô vàng các sản phẩm đa dạng mẫu mã trên thị trường hiện nay. Thì trong bài viết này Miluxinh sẽ xin gợi ý đến bạn món đồ chơi chó ngôi sao cao su đặc nhiều màu. Chắc chắn đây là sẽ món đồ chơi mà khiến thú cưng của bạn mê mẩn suốt ngày.  ', 1000, 56000, 1, 6, 1, 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (7, N'Bàn chải tắm silicon gấu trúc', N'Bàn chải silicon mềm mại giúp làm sạch chó mèo, không gây đau rát', 2000, 42000, 1, 7, 1, 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (8, N'Sữa tắm Yoko cao cấp 55ml', N'Với sự kết hợp tinh tế từ những hương thơm dịu nhẹ của hoa cỏ, YOKO tin rằng sẽ đem lại cảm giác tươi mát, năng động nhưng xen lẫn một chút ấm áp, giúp thú cưng luôn thoải mái và thơm tho mọi lúc mọi nơi.

Lấy cảm hứng từ mùa Xuân, Hạ, Thu, Đông, sữa tắm YOKO tin rằng sẽ đem đến trải nghiệm khó quên theo từng mùa trong năm. Đồng thời với các dưỡng chất thiên nhiên kết hợp vitamin giúp nuôi dưỡng da và lông mềm mượt, khử mùi, kháng khuẩn, lưu hương lâu và đặc biệt an toàn với thú cưng của bạn.

Cảm ơn bạn đã là những khách hàng đầu tiên trải nghiệm và tin chọn YOKO. Đừng quên cùng YOKO lan tỏa yêu thương và chăm sóc đặc biệt nhất dành cho thú cưng của bạn thời gian tới nữa nha!', 1000, 23000, 2, 8, 1, 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (9, N'Xịt Bioline khử trùng', N'Xịt khử trùng kháng khuẩn Bioline 500ml
Chai xịt làm sạch mùi hôi diệt khuẩn, diệt mầm bệnh, sát trùng không gian
Tinh dầu thông mang lại cảm giác mát lạnh, sạch sẽ.
Khử mùi tuyệt đối mùi khó chịu của chó mèo
Giúp triệt tiêu vi khuẩn, giữ không gian trong lành, mát mẻ
Tiêu diệt vi khuẩn trong không gian sống, không gian ở của thú cưng ở
Tiêu diệt virus, nấm, trong chuồng nuôi hay các nơi dơ bẩn
Hdsd: Xịt trực tiếp vào vị trí muốn xịt hoặc trên cơ thể vật nuôi', 500, 32000, 2, 8, 1, 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (10, N'Nhà vệ sinh cho mèo cửa tròn', N'1. Trọng lượng: 1.7kg
2. Chất liệu: nhựa PP an toàn, không gây kích ứng
3. Xuất xứ: Thượng Hải
4. Kích thước: 34*41*38CM
5. Thiết kế rộng rãi có thể dùng được cho mèo trên 10kg
ƯU ĐIỂM NHÀ VỆ SINH CHO MÈO
- Dễ dàng lắp ráp
- Thiết kế 2 cửa tiện lợi tuỳ theo sở thích mọi bé mèo

- Thiết kế keo khay cát, dễ dàng dọn dẹp thay cát. Không cần mở nắp

- Có cửa ngăn giúp hạn chế phát tán mùi hôi và tránh văng cát khi mèo lấp

- Bệ ra vào thiết kế lổ tròn: trong quá trình mèo ra vào, cát vệ sinh dính vào lòng bàn tay chân sẽ rơi ra tại đây. Giúp mèo giữ gìn vệ sinh, không bị liếm phải cát, và hạn chế viêm da

- Mèo nhỏ vẫn có thể sử dụng dễ dàng. 

Nhà vệ sinh vừa với mèo trên 10kg', 500, 267000, 2, 8, 1, 3)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (11, N'Cát đậu nành Cature Domestic 6L ', N'Đặc biệt: Giá thành của sản phẩm này mềm hơn so với thị trường rất nhiều 
😘Đương nhiên không vì thế mà chất lượng kém đi đâu ạ, thậm chí còn rất được ưa chuộng vì những tính năng và công dụng tuyệt vời 👍 của sản phẩm: 
😍Đầu tiên, phải nói đến sự thấm hút đỉnh cao và khử mùi siêu siêu tốt, khóa chặt "chất thải vệ sinh" cực tốt. Vón cục nhanh trong vòng 3s. Tính năng này thì ẻm là lựa chọn số 1️⃣
😍Thành phần hoàn toàn tự nhiên, không gây kích ứng với những bé mèo nhạy cảm. 
😍Hoàn toàn thân thiện với môi trường, có thể xả trong toilet, phân hủy sinh học. 
😍Ngoài ra, cát hoàn toàn không gây bụi và không dính chân mèo, không lo bẩn nhà nhé các Sen ơi
👉Có 2 dòng sản phẩm: 
💛Màu vàng: Cát đậu nành, hoàn toàn tự nhiên, khử mùi và vi khuẩn cực tốt', 1000, 120000, 2, 8, 1, 2)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Describe], [Quantity], [Price], [WarehouseID], [CategoryID], [Status], [SuppliersID]) VALUES (12, N'Chuồng mèo hai tầng Petland', N'Chuồng mèo siêu cao cấp kích thước lớn, có tầng để mèo thoải mái leo trèo và hoạt động. Một ngôi nhà an toàn và hoàn hảo cho mèo khi bạn phải ra ngoài không có thời gian chăm giữ bé. Kích thước: M - 75*54*170cm

Ngoài ra, lồng có bánh xe và khay nhựa dễ dàng vệ sinh chỉ với vài thao tác tháo lắp đơn giản

Chất liệu khung sắt tĩnh điện kết hợp với nhựa PVC cao cấp, chắc chắn, màu sắc bắt mắt, sang trọng', 800, 180000, 2, 9, 1, 3)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([SuppliersID], [SuppliersName], [Address], [Phone], [Email]) VALUES (1, N'WinMart', N'Số 20, Ngõ 123, Đường Trần Khát Chân, Hai Bà Trưng, Hà Nội', N'0912345678', N'winmart@gmail.com                                                                                                                                     ')
INSERT [dbo].[Suppliers] ([SuppliersID], [SuppliersName], [Address], [Phone], [Email]) VALUES (2, N'Lavil', N'Số 15, Ngõ 456, Đường Kim Mã, Ba Đình, Hà Nội', N'0987654321', N'lavil@gmail.com                                                                                                                                       ')
INSERT [dbo].[Suppliers] ([SuppliersID], [SuppliersName], [Address], [Phone], [Email]) VALUES (3, N'ShopPet', N'Số 30, Ngõ 789, Đường Xuân Thủy, Cầu Giấy, Hà Nội', N'0909090909', N'shoppet@gmail.com                                                                                                                                     ')
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO
SET IDENTITY_INSERT [dbo].[Warehouses] ON 

INSERT [dbo].[Warehouses] ([WarehouseID], [WarehouseName], [Address]) VALUES (1, N'Kho 001', N'Số 10, ngõ 230, Xuân Thủy, Cầu Giấy, Hà Nội')
INSERT [dbo].[Warehouses] ([WarehouseID], [WarehouseName], [Address]) VALUES (2, N'Kho 002', N'Số 9, Ngõ 777, Đường Trương Định, Hoàng Mai, Hà Nội')
SET IDENTITY_INSERT [dbo].[Warehouses] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Customer]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Manager] FOREIGN KEY([ManagerID])
REFERENCES [dbo].[Manager] ([ManagerID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Manager]
GO
ALTER TABLE [dbo].[InvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetails_Invoices] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoices] ([InvoicesID])
GO
ALTER TABLE [dbo].[InvoiceDetails] CHECK CONSTRAINT [FK_InvoiceDetails_Invoices]
GO
ALTER TABLE [dbo].[InvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetails_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[InvoiceDetails] CHECK CONSTRAINT [FK_InvoiceDetails_Products]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Customer]
GO
ALTER TABLE [dbo].[Prodcuts_Warehouses]  WITH CHECK ADD  CONSTRAINT [FK_Prodcuts_Warehouses_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Prodcuts_Warehouses] CHECK CONSTRAINT [FK_Prodcuts_Warehouses_Products]
GO
ALTER TABLE [dbo].[Prodcuts_Warehouses]  WITH CHECK ADD  CONSTRAINT [FK_Prodcuts_Warehouses_Warehouses] FOREIGN KEY([WarehouseID])
REFERENCES [dbo].[Warehouses] ([WarehouseID])
GO
ALTER TABLE [dbo].[Prodcuts_Warehouses] CHECK CONSTRAINT [FK_Prodcuts_Warehouses_Warehouses]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Category]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY([SuppliersID])
REFERENCES [dbo].[Suppliers] ([SuppliersID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Suppliers]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Warehouses] FOREIGN KEY([WarehouseID])
REFERENCES [dbo].[Warehouses] ([WarehouseID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Warehouses]
GO
ALTER TABLE [dbo].[ReceiptDetails]  WITH CHECK ADD  CONSTRAINT [FK_ReceiptDetails_Products] FOREIGN KEY([ReceiptID])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[ReceiptDetails] CHECK CONSTRAINT [FK_ReceiptDetails_Products]
GO
ALTER TABLE [dbo].[ReceiptDetails]  WITH CHECK ADD  CONSTRAINT [FK_ReceiptDetails_StockReceipts] FOREIGN KEY([ReceiptID])
REFERENCES [dbo].[StockReceipts] ([ReceiptID])
GO
ALTER TABLE [dbo].[ReceiptDetails] CHECK CONSTRAINT [FK_ReceiptDetails_StockReceipts]
GO
USE [master]
GO
ALTER DATABASE [Warehouses] SET  READ_WRITE 
GO
