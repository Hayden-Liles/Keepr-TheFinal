import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultsService{
    async getVaultById(id){
        AppState.activeVault = null
        AppState.keeps = []
        const res = await api.get('api/vaults/' + id)
        const res2 = await api.get('api/vaults/' + id + '/keeps')
        AppState.activeVault = res.data
        AppState.keeps = res2.data
    }
}

export const vaultsService = new VaultsService()