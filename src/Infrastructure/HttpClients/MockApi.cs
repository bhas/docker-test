using Application.HttpClients;
using Application.HttpClients.Dtos;
using System.Text.Json;

namespace Infrastructure.HttpClients;
public class MockApi : IAssetApi, IBriefingApi, IOrderListApi
{
    public Task<IReadOnlyList<AssetDto>> GetAssetsAsync(HashSet<string> assetIds, HashSet<string> folderIds)
    {
        var result = new List<AssetDto>
        {
            new() {
                AssetId = "ASSET001",
                Name = "Millennium Falcon Concept Art",
                Description = "High-resolution concept art of the Millennium Falcon spaceship used in the Star Wars films.",
                FileFormat = ".png",
                FileSize = 2035052,
                Path = "/path/to/asset001.jpg"
            },
            new() {
                AssetId = "ASSET002",
                Name = "Lightsaber Prop",
                Description = "Photographs of a replica lightsaber prop used in Star Wars cosplay.",
                FileFormat = ".jpg",
                FileSize = 9884561,
                Path = "/path/to/asset002.jpg"
            },
            new() {
                AssetId = "ASSET003",
                Name = "Darth Vader Costume\"",
                Description = "Images showcasing a detailed Darth Vader costume with helmet, cape, and armor.",
                FileFormat = ".jpg",
                FileSize = 8944521,
                Path = "/path/to/asset003.jpg"
            },
        };
        return Task.FromResult<IReadOnlyList<AssetDto>>(result!);
    }

    public Task<IReadOnlyList<BriefingDto>> GetBriefingsAsync(HashSet<string> names)
    {
        var result = new List<BriefingDto>
        {
            new() {
                Name = "Millennium Falcon Concept Art",
                Description = "High-resolution concept art of the Millennium Falcon spaceship used in the Star Wars films.",
                CreatedBy = "John Doe",
                CreatedDate = DateTimeOffset.Parse("2023-07-01")
            },
            new() {
                Name = "Lightsaber Prop",
                Description = "Photographs of a replica lightsaber prop used in Star Wars cosplay.",
                CreatedBy = "Jane Smith",
                CreatedDate = DateTimeOffset.Parse("2023-07-02")
            },
            new() {
                Name = "Darth Vader Costume",
                Description = "Images showcasing a detailed Darth Vader costume with helmet, cape, and armor.",
                CreatedBy = "Mark Johnson",
                CreatedDate = DateTimeOffset.Parse("2023-07-03")
            },
        };
        return Task.FromResult<IReadOnlyList<BriefingDto>>(result!);
    }

    public Task<OrderListDto> GetOrderListAsync(string orderNumber)
    {
        var result = new OrderListDto
        {
            OrderNumber = "ORD123456789",
            CustomerName = "John Doe",
            OrderDate = DateTimeOffset.Parse("2023-07-13"),
            TotalAssets = 3,
            Assets = [
                new OrderListAssetDto
                {
                    AssetId = "ASSET001",
                    Quantity = 2
                },
                new OrderListAssetDto
                {
                    AssetId = "ASSET002",
                    Quantity = 1
                },
                new OrderListAssetDto
                {
                    AssetId = "ASSET003",
                    Quantity = 3
                }
            ]
        };
        return Task.FromResult(result!);
    }
}
