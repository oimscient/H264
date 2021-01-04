
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Beidou.JT808
{
    #region 数据结构
    public struct UInt32Bytes
    {
        public UInt32 Value;
        public byte[] BytesValue;
    }

    public struct UInt32Byte
    {
        public UInt32 Value;
        public byte ByteValue;
    }

    public struct ByteBytes
    {
        public byte Value;
        public byte[] BytesValue;
    }

    public struct ByteString
    {
        public byte Value;
        public string StringValue;
    }

    public struct UInt32UInt32
    {
        public UInt32 Value;
        public UInt32 UInt32Value;
    }

    #endregion

    #region JT808协议 Receive操作结构信息
    /// <summary>
    /// 终端通用应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0001
    {
        /// <summary>
        /// 对应的应答流水号
        /// </summary>
        public UInt16 serialNumber;
        /// <summary>
        /// 对应的消息Id
        /// </summary>
        public UInt16 messageId;
        /// <summary>
        /// 结果0：成功/确认；1：失败；2：消息有误；3：不支持
        /// </summary>
        public byte result;
    }
    /// <summary>
    /// 终端心跳应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0002
    {
        /// <summary>
        /// 对应的应答流水号
        /// </summary>
        public UInt16 serialNumber;
        /// <summary>
        /// 对应的消息Id
        /// </summary>
        public UInt16 messageId;
        /// <summary>
        /// 结果0：成功/确认；1：失败；2：消息有误；3：不支持
        /// </summary>
        public byte result;
    }
    /// <summary>
    /// 终端注册
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0100
    {
        /// <summary>
        /// 省域ID
        /// </summary>
        public UInt16 ProvincialId;
        /// <summary>
        /// 市域ID
        /// </summary>
        public UInt16 CityId;
        /// <summary>
        /// 制造商ID,5个字节
        /// </summary>
        public byte[] ManufacturerId;
        /// <summary>
        /// 终端型号，20个字节
        /// </summary>
        public byte[] TerminalModel;
        /// <summary>
        /// 终端ID,7个字节
        /// </summary>
        public byte[] TerminalId;
        /// <summary>
        /// 车辆颜色
        /// </summary>
        public byte ColorPlates;
        /// <summary>
        /// 车辆标识(车牌号)
        /// </summary>
        public string VehicleIdentification;
    }

    /// <summary>
    /// 终端鉴权
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0102
    {
        /// <summary>
        /// 鉴权码
        /// </summary>
        public string AuthenticationCode;
    }

    /// <summary>
    /// 查询终端参数应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0104
    {
        /// <summary>
        /// 对应的应答流水号
        /// </summary>
        public UInt16 SerialNumber;
        /// <summary>
        /// 参数项集合：参数Id和数据值,参数id对应的值类型具体详见jt/t808协议的对应消息ID说明
        /// </summary>
        public List<UInt32Bytes> Items;
    }

    /// <summary>
    /// 查询终端属性应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0107
    {
        /// <summary>
        ///bit0，0：不适用客运车辆，1：适用客运车辆；
        ///bit1，0：不适用危险品车辆，1：适用危险品车辆；
        ///bit2，0：不适用普通货运车辆，1：适用普通货运车辆；
        ///bit3，0：不适用出租车辆，1：适用出租车辆；
        ///bit6，0：不支持硬盘录像，1：支持硬盘录像；
        ///bit7，0：一体机，1：分体机
        /// </summary>
        public UInt16 TerminalType;
        /// <summary>
        /// 制造商ID,5个字节
        /// </summary>
        public byte[] ManufacturerId;
        /// <summary>
        /// 终端型号，20个字节
        /// </summary>
        public byte[] TerminalModel;
        /// <summary>
        /// 终端ID,7个字节
        /// </summary>
        public byte[] TerminalId;
        /// <summary>
        /// 终端SIM卡,ICCID,10字节
        /// </summary>
        public byte[] TerminalSIM;
        /// <summary>
        /// 终端硬件版本号
        /// </summary>
        public string TerminalHardwareVersionNumber;
        /// <summary>
        /// 终端固件版本号
        /// </summary>
        public string TerminalFirmwareVersionNumber;
        /// <summary>
        /// Gnss模块属性
        /// bit0，0：不支持 GPS 定位， 1：支持 GPS 定位；
        ///bit1，0：不支持北斗定位， 1：支持北斗定位；
        ///bit2，0：不支持 GLONASS 定位， 1：支持 GLONASS 定位；
        ///bit3，0：不支持 Galileo 定位， 1：支持 Galileo 定位
        /// </summary>
        public byte GnssModuleProperties;
        /// <summary>
        /// 通信模块属性
        /// bit0，0：不支持GPRS通信， 1：支持GPRS通信；
        ///bit1，0：不支持CDMA通信， 1：支持CDMA通信；
        ///bit2，0：不支持TD-SCDMA通信， 1：支持TD-SCDMA通信；
        ///bit3，0：不支持WCDMA通信， 1：支持WCDMA通信；
        ///bit4，0：不支持CDMA2000通信， 1：支持CDMA2000通信。
        ///bit5，0：不支持TD-LTE通信， 1：支持TD-LTE通信；
        ///bit7，0：不支持其他通信方式， 1：支持其他通信方式
        /// </summary>
        public byte CommunicationModuleProperties;
    }

    /// <summary>
    /// 终端升级结果通知
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0108
    {
        /// <summary>
        ///升级类型
        /// 0：终端，12：道路运输证 IC 卡读卡器，52：北斗
        ///卫星定位模块
        /// </summary>
        public byte UpdateType;
        /// <summary>
        /// 升级结果,0：成功，1：失败，2：取消
        /// </summary>
        public byte UpdateResult;
    }

    /// <summary>
    /// 位置信息汇报
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0200
    {
        /// <summary>
        /// 报警标识，二进制各个位的表示见协议jt/t808协议说明
        /// </summary>
        public UInt32 AlarmIndication;
        /// <summary>
        /// 状态标识，二进制各个位的表示见协议jt/t808协议说明
        /// </summary>
        public UInt32 StatusIndication;
        /// <summary>
        /// 纬度,以度为单位的纬度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public UInt32 Latitude;
        /// <summary>
        /// 经度,以度为单位的经度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        public UInt32 Longitude;
        /// <summary>
        /// 海拔高度(m)
        /// </summary>
        public UInt16 Altitude;
        /// <summary>
        /// 速度,1/10km/h
        /// </summary>
        public UInt16 Speed;
        /// <summary>
        /// 方向,0-359，正北为 0，顺时针
        /// </summary>
        public UInt16 Direction;
        /// <summary>
        /// 定位时间BCD码,YY-MM-DD-hh-mm-ss（GMT+8 时间，本标准中之后涉及的时间均采用此时区）
        /// </summary>
        public byte[] LocationTime;
        /// <summary>
        /// 附加信息项,信息ID和数值,id对应的数值说明详细见jt/t808协议说明
        /// </summary>
        public List<ByteBytes> AttachItems;
    }

    /// <summary>
    /// RTP包
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RTPBody
    {
        /// <summary>
        /// 帧头
        /// </summary>
        public byte[] state;
        /// <summary>
        /// VPXCC
        /// </summary>
        public byte Vpxc;
        /// <summary>
        /// MPT
        /// </summary>
        public byte MPT;
        /// <summary>
        /// 包序号
        /// </summary>
        public UInt16 index;
        /// <summary>
        /// BCD码的Sim卡号,默认分配6个字节空间
        /// </summary>
        public byte[] hSimNumber;
        /// <summary>
        /// 逻辑通道
        /// </summary>
        public byte chanle;
        /// <summary>
        /// 数据类型、处理标记
        /// </summary>
        public byte type;
        /// <summary>
        /// 时间戳、8字节
        /// </summary>
        public byte[] time;
        /// <summary>
        /// 数据长度
        /// </summary>
        public UInt16 length;
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] data;
    }


    /// <summary>
    /// 音视频
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Video
    {
        /// <summary>
        /// 帧头标识
        /// </summary>
        public UInt32 state;
        /// <summary>
        /// V_P_X_CC；2bits++bits+1bits+4bits
        /// </summary>
        public byte V_P_X_CC;
        /// <summary>
        /// M_PT；1bits+7bits
        /// </summary>
        public byte M_PT;
        /// <summary>
        /// 包序号
        /// </summary>
        public uint num;
        /// <summary>
        /// SIM卡号
        /// </summary>
        public byte[] SIM;
        /// <summary>
        /// 逻辑通道
        /// </summary>
        public byte ID;
        /// <summary>
        ///数据类型，分包处理标记；4bits+4bits
        /// </summary>
        public byte type;
        /// <summary>
        /// 时间戳；8位
        /// </summary>
        public byte[] Time;
        /// <summary>
        /// 与上一关键帧时间间隔，数据类型为0100时没有该字段
        /// </summary>
        public UInt16 Last_I_F;
        /// <summary>
        /// 与上一关时间间隔，数据类型为0100时没有该字段
        /// </summary>
        public UInt16 Last_F;
        /// <summary>
        /// 数据体长度
        /// </summary>
        public UInt16 length;
        /// <summary>
        /// 数据体
        /// </summary>
        public byte[] data;
    }


    /// <summary>
    /// 位置信息查询应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0201
    {
        /// <summary>
        /// 对应的应答流水号
        /// </summary>
        public UInt16 SerialNumber;
        /// <summary>
        /// 位置信息汇报
        /// </summary>
        public PB0200 PositionInformation;
    }

    /// <summary>
    /// 事件报告
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0301
    {
        /// <summary>
        /// 事件Id
        /// </summary>
        public byte EventId;
    }

    /// <summary>
    /// 提问应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0302
    {
        /// <summary>
        /// 对应的应答流水号
        /// </summary>
        public UInt16 SerialNumber;
        /// <summary>
        /// 提问下发中附带的答案 Id
        /// </summary>
        public byte AnswerId;
    }

    /// <summary>
    /// 信息点播/取消
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0303
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public byte MessageType;
        /// <summary>
        /// 0：取消；1：点播
        /// </summary>
        public byte OperationIdentity;
    }

    /// <summary>
    /// 车辆控制应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0500
    {
        /// <summary>
        /// 对应的应答流水号
        /// </summary>
        public UInt16 SerialNumber;
        /// <summary>
        /// 位置信息汇报
        /// </summary>
        public PB0200 PositionInformation;
    }

    /// <summary>
    /// 行驶记录数据上传
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0700
    {
        /// <summary>
        /// 对应的应答流水号
        /// </summary>
        public UInt16 SerialNumber;
        /// <summary>
        /// 对应平台发出的命令字
        /// </summary>
        public byte Command;
        /// <summary>
        /// 数据内容,数据块内容格式见 GB/T 19056 中相关内容，包含
        ///GB/T 19056 要求的完整数据包
        /// </summary>
        public byte[] DataContent;
    }

    /// <summary>
    /// 电子运单
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0701
    {
        /// <summary>
        /// 电子运单数据包
        /// </summary>
        public byte[] ElectronicWaybill;
    }

    /// <summary>
    /// 驾驶员身份信息采集上报
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0702
    {
        /// <summary>
        /// 状态
        /// 0x01：从业资格证 IC 卡插入（驾驶员上班）；
        ///0x02：从业资格证 IC 卡拔出（驾驶员下班）。
        /// </summary>
        public byte Status;
        /// <summary>
        /// 时间，6位BCD码
        /// </summary>
        public byte[] Time;
        /// <summary>
        /// IC读卡结果
        /// 0x00：IC 卡读卡成功；
        ///0x01：读卡失败，原因为卡片密钥认证未通过；
        ///0x02：读卡失败，原因为卡片已被锁定；
        ///0x03：读卡失败，原因为卡片被拔出；
        ///0x04：读卡失败，原因为数据校验错误。
        ///以下字段在 IC 卡读取结果等于 0x00 时才有效。
        /// </summary>
        public byte ICReaderResult;
        /// <summary>
        ///驾驶员姓名
        /// </summary>
        public string DriverName;
        /// <summary>
        /// 从业资格证编码
        /// </summary>
        public string QualificationCertificateCoding;
        /// <summary>
        /// 证书机构名称
        /// </summary>
        public string CertificateAuthorityName;
        /// <summary>
        /// 证件期限,4位BCD码
        /// </summary>
        public byte[] CertificateDeadline;
    }

    /// <summary>
    /// 定位数据批量上传
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0704
    {
        /// <summary>
        /// 位置数据类型,0：正常位置批量汇报，1：盲区补报
        /// </summary>
        public byte LocationDataTypes;
        /// <summary>
        /// 位置信息汇报数据项集合
        /// </summary>
        public List<PB0200> PositionInformationItems;
    }

    /// <summary>
    /// CAN 总线数据上传
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0705
    {
        /// <summary>
        /// CAN总线数据接收时间,5字节
        /// </summary>
        public byte[] CANBusDataReceptionTime;
        /// <summary>
        /// CAN总线数据项集合
        /// </summary>
        public List<CANItem> CANItems;
    }

    /// <summary>
    /// CAN总线数据项
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CANItem
    {
        /// <summary>
        /// CAN总线编码,4字节
        /// bit31 表示 CAN 通道号，0：CAN1，1：CAN2；
        ///bit30 表示帧类型，0：标准帧，1：扩展帧；
        ///bit29 表示数据采集方式，0：原始数据，1：采
        ///集区间的平均值；
        ///bit28-bit0 表示 CAN 总线 ID。
        /// </summary>
        public byte[] CANId;
        /// <summary>
        /// CAN总线数据,8字节
        /// </summary>
        public byte[] CANData;
    }

    /// <summary>
    /// 多媒体事件信息上传
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0800
    {
        /// <summary>
        /// 多媒体数据Id
        /// </summary>
        public UInt32 MultimediaDataId;
        /// <summary>
        /// 多媒体类型
        /// 0：图像；1：音频；2：视频；
        /// </summary>
        public byte MultmediaType;
        /// <summary>
        /// 多媒体格式编码
        /// 0：JPEG；1：TIF；2：MP3；3：WAV；4：WMV；其他保留
        /// </summary>
        public byte MultimediaFormat;
        /// <summary>
        /// 事件项编码
        /// 0：平台下发指令；1：定时动作；2：抢劫报警触
        ///发；3：碰撞侧翻报警触发；4：门开拍照；5：门
        ///关拍照；6：车门由开变关，时速从＜20 公里到超
        ///过 20 公里；7：定距拍照；
        ///其他保留
        /// </summary>
        public byte EventItemCoding;
        /// <summary>
        /// 通道ID
        /// </summary>
        public byte ChannelId;
    }

    /// <summary>
    /// 多媒体数据上传
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0801
    {
        /// <summary>
        /// 多媒体数据Id
        /// </summary>
        public UInt32 MultimediaDataId;
        /// <summary>
        /// 多媒体类型
        /// 0：图像；1：音频；2：视频；
        /// </summary>
        public byte MultmediaType;
        /// <summary>
        /// 多媒体格式编码
        /// 0：JPEG；1：TIF；2：MP3；3：WAV；4：WMV；其他保留
        /// </summary>
        public byte MultimediaFormat;
        /// <summary>
        /// 事件项编码
        ///0：平台下发指令；1：定时动作；2：抢劫报警触发；3：碰撞侧翻报警触发；其他保留
        /// </summary>
        public byte EventItemCoding;
        /// <summary>
        /// 通道ID
        /// </summary>
        public byte ChannelId;
        /// <summary>
        /// 位置信息汇报
        /// </summary>
        public PB0200 PositionInformation;
        /// <summary>
        /// 多媒体数据包
        /// </summary>
        public byte[] MultimediaPackage;
    }

    /// <summary>
    /// 存储多媒体数据检索应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0802
    {
        /// <summary>
        /// 对应的应答流水号
        /// </summary>
        public UInt16 SerialNumber;
        /// <summary>
        /// 多媒体检索项集合
        /// </summary>
        public List<IndexItem> MultimediaIndexItems;
    }

    /// <summary>
    /// 检索项
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct IndexItem
    {
        /// <summary>
        /// 多媒体数据Id
        /// </summary>
        public UInt32 MultimediaDataId;
        /// <summary>
        /// 多媒体类型
        /// 0：图像；1：音频；2：视频；
        /// </summary>
        public byte MultmediaType;
        /// <summary>
        /// 通道ID
        /// </summary>
        public byte ChannelId;
        /// <summary>
        /// 事件项编码
        ///0：平台下发指令；1：定时动作；2：抢劫报警触发；3：碰撞侧翻报警触发；其他保留
        /// </summary>
        public byte EventItemCoding;
        /// <summary>
        /// 位置信息汇报
        /// </summary>
        public PB0200 PositionInformation;
    }

    /// <summary>
    /// 摄像头立即拍摄命令应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0805
    {
        /// <summary>
        /// 对应的应答流水号
        /// </summary>
        public UInt16 SerialNumber;
        /// <summary>
        /// 0：成功；1：失败；2：通道不支持。以下字段在结果=0 时才有效。
        /// </summary>
        public byte Result;
        /// <summary>
        /// 多媒体ID列表,拍摄成功的ID列表
        /// </summary>
        public List<UInt32> MultimediaIdItems;
    }

    /// <summary>
    /// 数据上行透传
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0900
    {
        ///<summary>
        ///透传类型
        ///0x00:GNSS 模块详细定位数据
        ///0x0b:道路运输证 IC 卡信息上传消息为 64Byte，下传消息为24Byte。道路运输证 IC 卡认证透传超时时间为 30s。超时后，不重发
        ///0x41:串口 1 透传消息
        ///0x42:串口 2 透传消息
        ///0xF0-0xFF:用户自定义透传消息
        /// </summary>
        public byte PassthroughType;
        /// <summary>
        /// 数据透传内容
        /// </summary>
        public byte[] PassThroughContent;
    }

    /// <summary>
    /// 数据压缩上报
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0901
    {
        /// <summary>
        /// 经过GZIP压缩的压缩数据
        /// </summary>
        public byte[] CompressData;
    }

    /// <summary>
    /// 终端 RSA 公钥
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB0A00
    {
        /// <summary>
        /// 终端 RSA 公钥{e,n}中的 e
        /// </summary>
        public UInt32 RSA_e;
        /// <summary>
        /// RSA 公钥{e,n}中的 n,128字节
        /// </summary>
        public byte[] RSA_n;
    }

    /// <summary>
    ///  消息指令0x8001(平台通用应答)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8001
    {
        /// <summary>
        /// 对应收到的流水号
        /// </summary>
        public UInt16 Serialnumber;
        /// <summary>
        /// 对应收到的消息ID
        /// </summary>
        public UInt16 MessageId;
        /// <summary>
        /// 处理结果,0成功/确认,1失败,2消息有误,3不支持,4(平台)报警确认
        /// </summary>
        public byte Result;
    }

    /// <summary>
    /// 消息指令0x8003 补传分包请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8003
    {
        /// <summary>
        /// 第一个包的消息流水号
        /// </summary>
        public UInt16 Serialnumber;
        /// <summary>
        /// 重传包ID列表,<=255
        /// </summary>
        public List<UInt16> IDList;
    }

    /// <summary>
    ///  终端注册应答
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8100
    {
        /// <summary>
        /// 对应的流水号
        /// </summary>
        public UInt16 Serialnumber;
        /// <summary>
        /// 应答结果0：成功；1：车辆已被注册；2：数据库中无该车辆；
        ///3：终端已被注册；4：数据库中无该终端
        /// </summary>
        public byte Result;
        /// <summary>
        /// 鉴权码
        /// </summary>
        public string AuthenticationCode;
    }

    /// <summary>
    /// 设置终端参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8103
    {
        /// <summary>
        /// 参数集合,相应的值设置可以查看jt808协议中消息ID：0x8103的说明
        /// </summary>
        public List<UInt32Bytes> Parameters;
    }

    /// <summary>
    /// 消息指令0x8105(终端控制)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8105
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public byte Cmd;
        /// <summary>
        /// 命令字参数
        /// </summary>
        public byte[] CmdParameters;
    }

    /// <summary>
    /// 0x8106消息指令 查询指定终端参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8106
    {
        /// <summary>
        /// 参数ID列表
        /// </summary>
        public List<UInt32> IDList;
    }

    /// <summary>
    /// 终端升级数据包信息结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8108
    {
        /// <summary>
        /// 升级类型,0：终端，12：道路运输证 IC 卡读卡器，52：北斗卫星定位模块
        /// </summary>
        public byte updateType;
        /// <summary>
        /// 制造商ID，5个字节
        /// </summary>
        public byte[] manufacturersNo;
        /// <summary>
        /// 版本号
        /// </summary>
        public string version;
        /// <summary>
        /// 数据包
        /// </summary>
        public byte[] updateData;
    }

    /// <summary>
    /// 临时位置跟踪控制
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8202
    {
        /// <summary>
        /// 时间间隔(单位秒)0表示停止跟踪
        /// </summary>
        public UInt16 tInterval;
        /// <summary>
        /// 位置跟踪有效期(单位秒)位置跟踪上报间隔
        /// </summary>
        public UInt32 tValidTime;
    }

    /// <summary>
    ///   消息指令0x8203(人工确认报警消息)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8203
    {
        /// <summary>
        /// 对应的消息流水号
        /// </summary>
        public UInt16 Serialnumber;
        /// <summary>
        /// (0-1)1确认紧急报警
        /// </summary>
        public byte sureUrgentAlarm;
        /// <summary>
        /// (0-1)1确认危险报警
        /// </summary>
        public byte sureDangerAlarm;
        /// <summary>
        /// (0-1)1确认进出区域报警
        /// </summary>
        public byte areaAlarm;
        /// <summary>
        /// (0-1)1确认进出路线报警
        /// </summary>
        public byte roadAlarm;
        /// <summary>
        /// 0-1)1确认路段行驶时间不足/过程报警
        /// </summary>
        public byte dtimeAlarm;
        /// <summary>
        /// (0-1)1确认车辆非法点火报警
        /// </summary>
        public byte illegalStartAlarm;
        /// <summary>
        /// (0-1)1确认车辆非法位移报警
        /// </summary>
        public byte illegalMoveAlarm;
    }

    /// <summary>
    ///  消息指令0x8300(文本下发指令)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8300
    {
        /// <summary>
        /// （0-1）1:紧急内容
        /// </summary>
        public byte EmFlag;
        /// <summary>
        /// （0-1）1:终端显示器显示
        /// </summary>
        public byte displayScreen;
        /// <summary>
        /// (0-1)1:文本到语音报读
        /// </summary>
        public byte tts;
        /// <summary>
        /// (0-1)1:广告屏显示
        /// </summary>
        public byte adScreen;
        /// <summary>
        /// (0-1)0:中心导航信息，1:CAN 故障码信息
        /// </summary>
        public byte msgType;
        /// <summary>
        /// 短信内容,最大值1024字节
        /// </summary>
        public string msgContent;
    }

    /// <summary>
    ///事件设置
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8301
    {
        /// <summary>
        /// 0：删除终端现有所有事件，该命令后不带后继字节；
        ///1：更新事件；
        ///2：追加事件；
        ///3：修改事件；
        ///4：删除特定几项事件，之后事件项中无需带事件内容
        /// </summary>
        public byte eventType;
        /// <summary>
        /// 事件项内容，(个数不超过255),详细间jt808的0x8301信息说明
        /// </summary>
        public List<ByteString> eventItems;
    }

    /// <summary>
    /// 提问下发
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8302
    {
        /// <summary>
        /// (0-1)1紧急信息
        /// </summary>
        public byte urgent;
        /// <summary>
        /// (0-1)1语音播报
        /// </summary>
        public byte tts;
        /// <summary>
        /// (0-1)1广告屏显示
        /// </summary>
        public byte showf;
        /// <summary>
        /// 问题内容
        /// </summary>
        public string question;
        /// <summary>
        /// 答案选项
        /// </summary>
        public List<ByteString> answerList;
    }

    /// <summary>
    /// 消息指令0x8303(信息点播菜单设置)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8303
    {
        /// <summary>
        /// 设置类型,0：删除全部信息项,1：更新信息项,2：追加信息项,3：修改信息项
        /// </summary>
        public byte SettingType;
        /// <summary>
        /// 信息项列表
        /// </summary>
        public List<ByteString> MessageList;
    }

    /// <summary>
    /// 消息指令0x8401(设置电话本)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8401
    {
        /// <summary>
        /// 0：删除终端上所有存储的联系人；
        ///1：表示更新电话本（删除终端中已有全部联系人
        ///并追加消息中的联系人） ；
        ///2：表示追加电话本；
        ///3：表示修改电话本（以联系人为索引）
        /// </summary>
        public byte sType;
        /// <summary>
        /// 电话本联系人项
        /// </summary>
        public List<_PB8401Item> ContactList;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct _PB8401Item
    {
        /// <summary>
        /// 标志,1呼入,2呼出,3呼入/呼出
        /// </summary>
        public byte flag;
        /// <summary>
        /// 电话号码
        /// </summary>
        public string mobile;
        /// <summary>
        /// 联系人
        /// </summary>
        public string contact;
    }

    /// <summary>
    /// 圆形区域结构信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8600
    {
        /// <summary>
        /// 设置属性,0更新,1追加,2修改
        /// </summary>
        public byte settingProperty;
        /// <summary>
        /// 圆形区域项集合
        /// </summary>
        public List<PB8600Item> circleItemsInfo;
    }

    /// <summary>
    /// 圆形区域项
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8600Item
    {
        /// <summary>
        /// 圆形Id
        /// </summary>
        public UInt32 circleId;
        /// <summary>
        /// 圆形区域属性
        /// </summary>
        public UInt16 circleProperty;
        /// <summary>
        /// 中心点纬度
        /// </summary>
        public UInt32 circleCenterLat;
        /// <summary>
        /// 中心点精度
        /// </summary>
        public UInt32 circleCenterLng;
        /// <summary>
        /// 圆形半径
        /// </summary>
        public UInt32 circleRadius;
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime stime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime etime;
        /// <summary>
        /// 最高速度
        /// </summary>
        public UInt16 maxSpeed;
        /// <summary>
        /// 超速持续时间
        /// </summary>
        public byte overSpeedingTime;
    }

    /// <summary>
    /// 区域属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AreaAttribute
    {
        /// <summary>
        /// 1:根据时间
        /// </summary>
        public byte accordingTime;
        /// <summary>
        /// 1:限速
        /// </summary>
        public byte limitSpeed;
        /// <summary>
        /// 1:进区域报警给驾驶员
        /// </summary>
        public byte inareaUpDriver;
        /// <summary>
        /// 1:进区域报警给平台
        /// </summary>
        public byte inareaUpPltf;
        /// <summary>
        /// 1:出区域报警给驾驶员
        /// </summary>
        public byte outareaUpDriver;
        /// <summary>
        /// 1:出区域报警给平台
        /// </summary>
        public byte outareaUpPltf;
        /// <summary>
        /// 0北纬,1南纬
        /// </summary>
        public byte latflag;
        /// <summary>
        /// 0东经,1西经
        /// </summary>
        public byte lngflag;
        /// <summary>
        /// 0允许开门,1禁止开门
        /// </summary>
        public byte openflag;
        /// <summary>
        /// 0进区域开启通信模块,1进区域关闭通信模块
        /// </summary>
        public byte communicationflag;
        /// <summary>
        /// 0进区域不采集MSGBody详细定位数据；1进区域采集MSGBody详细定位数据
        /// </summary>
        public byte samplingflag;
    }

    /// <summary>
    /// 矩形区域结构信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PB8602
    {
        /// <summary>
        /// 设置属性,0更新,1追加,2修改
        /// </summary>
        public byte settingProperty;
        /// <summary>
        /// 矩形区域项集合
        /// </summary>
        public List<PB8602Item> rectangleItemsInfo;
    }

    /// <summary>
    /// 矩形区域项
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8602Item
    {
        /// <summary>
        /// 矩形区域Id
        /// </summary>
        public UInt32 rectangleId;
        /// <summary>
        /// 矩形区域属性
        /// </summary>
        public UInt16 rectangleProperty;
        /// <summary>
        /// 矩形左上点纬度
        /// </summary>
        public UInt32 rectangleLeftTopLat;
        /// <summary>
        /// 矩形左上点经度
        /// </summary>
        public UInt32 rectangleLeftTopLng;
        /// <summary>
        /// 矩形右下点纬度
        /// </summary>
        public UInt32 rectangleRightDownLat;
        /// <summary>
        /// 矩形右下点经度
        /// </summary>
        public UInt32 rectangleRightDownLng;
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime stime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime etime;
        /// <summary>
        /// 最高速度
        /// </summary>
        public UInt16 maxSpeed;
        /// <summary>
        /// 超速持续时间
        /// </summary>
        public byte overSpeedingTime;
    }

    /// <summary>
    /// 多边形区域结构信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8604
    {
        /// <summary>
        /// 多边形Id
        /// </summary>
        public UInt32 polygonId;
        /// <summary>
        /// 多边形区域属性
        /// </summary>
        public UInt16 polygonProperty;
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime stime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime etime;
        /// <summary>
        /// 最高速度
        /// </summary>
        public UInt16 maxSpeed;
        /// <summary>
        /// 超速持续时间
        /// </summary>
        public byte overSpeedingTime;
        /// <summary>
        /// 拐点项,Value:纬度;UInt32Value:经度
        /// </summary>
        public List<UInt32UInt32> polygonItemsInfo;
    }
    /// <summary>
    /// 实时音视频传输请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB9101
    {
        /// <summary>
        /// 服务器ip地址长度
        /// </summary>
        public byte length;
        /// <summary>
        /// 服务器ip
        /// </summary>
        public string ip;
        /// <summary>
        /// 服务器端口号TCP
        /// </summary>
        public UInt16 port;
        /// <summary>
        /// 服务器端口号UDP
        /// </summary>
        public UInt16 ports;
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte id;
        /// <summary>
        /// 数据类型
        /// </summary>
        public byte datatype;
        /// <summary>
        /// 码流类型
        /// </summary>
        public byte datatypes;
    }
    /// <summary>
    /// 实时音视频传输请求
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB9102
    {
        /// <summary>
        /// 逻辑通道号
        /// </summary>
        public byte id;
        /// <summary>
        /// 控制指令
        /// </summary>
        public byte order;
        /// <summary>
        /// 操作类型
        /// </summary>
        public byte type;
        /// <summary>
        /// 码流类型
        /// </summary>
        public byte datatypes;
    }
    /// <summary>
    /// 路线结构信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8606
    {
        /// <summary>
        /// 路线ID
        /// </summary>
        public UInt32 routeId;
        /// <summary>
        /// 路线属性
        /// </summary>
        public UInt16 routeProperty;
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime stime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime etime;
        /// <summary>
        /// 拐点项总数
        /// </summary>
        public UInt16 inflectionPointCount;
        /// <summary>
        /// 拐点项集合
        /// </summary>
        public List<PB8606Item> Items;
    }

    /// <summary>
    /// 路线拐点项
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8606Item
    {
        /// <summary>
        /// 拐点ID
        /// </summary>
        public UInt32 inflectionPointId;
        /// <summary>
        /// 路段ID
        /// </summary>
        public UInt32 routeSectionId;
        /// <summary>
        /// 拐点纬度
        /// </summary>
        public UInt32 inflectionPointLat;
        /// <summary>
        /// 拐点经度
        /// </summary>
        public UInt32 inflectionPointLng;
        /// <summary>
        /// 路段宽度
        /// </summary>
        public byte routeSectionWidth;
        /// <summary>
        /// 路段属性
        /// </summary>
        public byte routeSectionProperty;
        /// <summary>
        /// 路段行驶过长阀值
        /// </summary>
        public UInt16 routeSectionLongTime;
        /// <summary>
        /// 路段行驶过短阀值
        /// </summary>
        public UInt16 routeSectionShortTime;
        /// <summary>
        /// 路段最高速度
        /// </summary>
        public UInt16 routeSectionMaxSpeed;
        /// <summary>
        /// 路段超速持续时间
        /// </summary>
        public byte routeSectionSpeedingTime;
    }

    /// <summary>
    /// 路段属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RoadSectionAttribute
    {
        /// <summary>
        /// 1:行驶时间
        /// </summary>
        public byte drivertime;
        /// <summary>
        /// 1:限速
        /// </summary>
        public byte limitspeed;
        /// <summary>
        /// 0:北纬;1:南纬
        /// </summary>
        public byte latflag;
        /// <summary>
        /// 0:东经;1:西经
        /// </summary>
        public byte lngflag;
    }

    /// <summary>
    /// 路线属性
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RoadAttribute
    {
        /// <summary>
        /// 1:根据时间
        /// </summary>
        public byte accordingtime;
        /// <summary>
        /// 1:进路线报警给驾驶员
        /// </summary>
        public byte inRouteUpDriver;
        /// <summary>
        /// 1:进路线报警给平台
        /// </summary>
        public byte inRouteUpPltf;
        /// <summary>
        /// 1:出路线报警给驾驶员
        /// </summary>
        public byte outRouteUpDriver;
        /// <summary>
        /// 1:出路线报警给平台
        /// </summary>
        public byte outRouteUpPltf;
    }

    /// <summary>
    ///消息指令0x8700(行驶记录数据采集命令)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8700
    {
        /// <summary>
        /// 命令字
        /// </summary>
        public byte Cmd;
        /// <summary>
        /// 数据项
        /// </summary>
        public PB8700Item item;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PB8700Item
    {
        /// <summary>
        /// 起始时间,格式如：2014120116141800
        /// </summary>
        public string StartTime;
        /// <summary>
        /// 结束时间,格式如：2014120116141800
        /// </summary>
        public string EndTime;
        /// <summary>
        /// 数据块最大个数
        /// </summary>
        public UInt16 MaxCount;
    }

    /// <summary>
    /// 消息指令0x8800(多媒体数据上传应答)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8800
    {
        /// <summary>
        /// 多媒体ID
        /// </summary>
        public UInt32 MediaId;
        /// <summary>
        /// 重传包ID列表
        /// </summary>
        public List<UInt16> pIdList;
    }

    /// <summary>
    /// 摄像头拍摄信息结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PB8801
    {
        /// <summary>
        /// 通道Id,>0
        /// </summary>
        public byte channelId = 1;
        /// <summary>
        /// 拍摄命令,0:停止拍照,0xFFFF:录像,其他为拍照张数
        /// </summary>
        public UInt16 photoCmd = 1;
        /// <summary>
        /// 拍照时间间隔,0:一直录像(秒)
        /// </summary>
        public UInt16 photoInterval = 5;
        /// <summary>
        /// 拍照存储标志,0:实时上传,1:保存在终端
        /// </summary>
        public byte photoSaveFlag = 0;
        /// <summary>
        /// 拍照分辨率,分辨率1：320*240,2：640*480,3：800*600,4：1024*768,
        /// 5：176*144,[Qcif];6：352*288;[Cif];7：704*288;[HALF D1];8：04*576;[D1];
        /// </summary>
        public byte photoResolution = 1;
        /// <summary>
        /// 拍照质量,1-10，1 代表质量损失最小，10 表示压缩比最大
        /// </summary>
        public byte photoQuality = 5;
        /// <summary>
        /// 拍照亮度,0-255
        /// </summary>
        public byte photoBrightness = 127;
        /// <summary>
        /// 拍照对比度,0-127
        /// </summary>
        public byte photoContrast = 86;
        /// <summary>
        /// 拍照饱和度,0-127
        /// </summary>
        public byte photoSaturation = 86;
        /// <summary>
        ///拍照色度,0-255
        /// </summary>
        public byte photoColor = 127;
    }

    /// <summary>
    /// 多媒体检索信息结构
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class PB8802
    {
        /// <summary>
        /// 多媒体类型,0:图片,1:音频,2:视频
        /// </summary>
        public byte mType;
        /// <summary>
        /// 检索媒体的通道,0表示所有通道
        /// </summary>
        public byte channelId;
        /// <summary>
        /// 事件编码,：平台下发指令；1：定时动作；2：抢劫报警触
        ///发；3：碰撞侧翻报警触发；其他保留
        /// </summary>
        public byte eventCode;
        /// <summary>
        /// 媒体数据起始时间
        /// </summary>
        public DateTime stime;
        /// <summary>
        /// 媒体数据结束时间
        /// </summary>
        public DateTime etime;
    }


    /// <summary>
    /// 多媒体上传数据结构信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8803
    {
        /// <summary>
        /// 多媒体类型,0:图像,1:音频,2:视频
        /// </summary>
        public byte mType;
        /// <summary>
        /// 多媒体通道号
        /// </summary>
        public byte channelId;
        /// <summary>
        /// 事件编码,0：平台下发指令；1：定时动作；2：抢劫报警触
        ///发；3：碰撞侧翻报警触发；其他保留
        /// </summary>
        public byte eventCode;
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime stime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime etime;
        /// <summary>
        /// 删除标志,0:保留,1:删除
        /// </summary>
        public byte delFlag;
    }

    /// <summary>
    /// 录音消息结构信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8804
    {
        /// <summary>
        /// 录音指令,0:停止录音,0x01:开始录音
        /// </summary>
        public byte recordingCmd;
        /// <summary>
        /// 录音时间(s),0:一直录音
        /// </summary>
        public UInt16 recordingTime;
        /// <summary>
        /// 保存标志,0:实时上传,1:保存
        /// </summary>
        public byte recordingSaveFlag;
        /// <summary>
        /// 录音采样频率，0：8K；1：11K；2：23K；3：32K；其他保留
        /// </summary>
        public byte recordingSampleFre;
    }

    /// <summary>
    ///消息指令0x8805(单条存储多媒体数据检索上传命令)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8805
    {
        /// <summary>
        /// 多媒体ID,必需大于0
        /// </summary>
        public UInt32 mediaId;
        /// <summary>
        /// 0：保留；1：删除
        /// </summary>
        public byte deleteFlag;
    }

    /// <summary>
    /// 消息指令0x8900(数据下行透传)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct PB8900
    {
        /// <summary>
        /// 41：串口1透传,46：串口2透传，240-255：用户自定义透传
        ///   消息类型,0：GNSS 模块详细定位数据,11：道路运输证IC 卡信息
        /// </summary>
        public byte mType;
        /// <summary>
        /// 对应消息类型的透传内容
        /// </summary>
        public byte[] content;
    }



    #endregion
}
