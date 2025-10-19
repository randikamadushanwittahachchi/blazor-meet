using Blazored.LocalStorage;
using ClientLibrary.Helper.Constracts;

namespace ClientLibrary.Helper.Implimentations;

public class LocalStorage : ILocalStorage
{
    private readonly ILocalStorageService _localStorageService;
    private const string StorageKey = "authentication-key";

    public LocalStorage(ILocalStorageService localStorageService)
        =>    _localStorageService = localStorageService;

    public async Task<string?> GetTokenAsync()
        => await _localStorageService.GetItemAsStringAsync(StorageKey);

    public async Task RemoveTokenAsync()
        => await _localStorageService.RemoveItemAsync(StorageKey);

    public async Task SetTokenAsync(string item)
        => await _localStorageService.SetItemAsStringAsync(StorageKey,item);
}
