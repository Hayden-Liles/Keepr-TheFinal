import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"


class KeepsService{

    async getAllKeeps(){
        const keeps = await api.get('api/keeps')
        AppState.keeps = keeps.data
    }

    async createKeep(newKeep){
        const keep = await api.post('api/keeps', newKeep)
        AppState.keeps.push(keep.data)
    }

    async getKeep(id){
        AppState.activeKeep = null;
        const keep = await api.get('api/keeps/' + id)
        AppState.activeKeep = keep.data
    }

    async deleteKeep(id){
        await api.delete('api/keeps/' + id)
        AppState.keeps = AppState.keeps.filter(k => k.id != id)
    }

    async addKeepToVault(vaultId, keepId){
        const vaultKeep = {
            vaultId: vaultId,
            keepId: keepId
        }
        AppState.activeKeep.kept += 1
        await api.post('api/vaultkeeps', vaultKeep)
    }

    async removeKeepFromVault(keepId){
        const keep = AppState.keeps.find(k => k.id == keepId)
        const keepIndex = AppState.keeps.findIndex(k => k.id == keepId)
        AppState.activeKeep.kept -= 1
        await api.delete('api/vaultkeeps/' + keep.vaultKeepId)
        AppState.keeps.splice(keepIndex, 1)
    }

}

export const keepsService = new KeepsService()