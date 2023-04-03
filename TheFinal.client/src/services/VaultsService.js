import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService{
    async getVaultById(id){
        AppState.activeVault = null
        AppState.keeps = []
        const res = await api.get('api/vaults/' + id)
        logger.log("hi")
        const res2 = await api.get('api/vaults/' + id + '/keeps')
        AppState.activeVault = res.data
        AppState.keeps = res2.data
    }

    async DeleteVault(id){
        await api.delete('api/vaults/' + id)
        AppState.vaults = AppState.vaults.filter(v => v.id != id)
    }

    async createVault(vaultData){
        const res = await api.post('api/vaults', vaultData)
        AppState.vaults.push(res.data)
    }
}

export const vaultsService = new VaultsService()