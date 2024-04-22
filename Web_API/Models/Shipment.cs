using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Shipment
{
    public Guid Id { get; set; }

    public int ShipmentId { get; set; }

    public DateTime? ShipmentDate { get; set; }

    public Guid? OrderId { get; set; }

    public string? PackageSize { get; set; }

    public string? PakegeLength { get; set; }

    public string? PakegeWidth { get; set; }

    public string? PakegeHeight { get; set; }

    public string? SizeUnitOfMesure { get; set; }

    public string? WeightUnitOfMeasure { get; set; }

    public string? TypeOfPackagingMaterial { get; set; }

    public string? TypeOfPackage { get; set; }

    public string? StoringTemparture { get; set; }

    public string? TemperatureUnitOfMeasure { get; set; }

    public string? NoOfPackages { get; set; }

    public string? TrackingNumber { get; set; }

    public DateTime? ExpectedDeliveryDate { get; set; }

    public Guid? ShipmentAddressId { get; set; }

    public string? ReceivingHours { get; set; }

    public string? ReceiverId { get; set; }

    public string? IsSignatureNeeded { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Address? ShipmentAddress { get; set; }
}
