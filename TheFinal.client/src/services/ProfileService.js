import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfileService{
    async getProfile(id){
        const res = await api.get('api/profiles/' + id)
        AppState.profile = res.data
    }

    async getProfileKeeps(profileId){
        const res = await api.get('api/profiles/' + profileId + '/keeps')
        AppState.keeps = res.data
    }

    async getProfileVaults(profileId){
        const res = await api.get('api/profiles/' + profileId + '/vaults')
        AppState.vaults = res.data
    }
}

export const profileService = new ProfileService()