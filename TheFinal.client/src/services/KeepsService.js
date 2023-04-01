import { AppState } from "../AppState"
import { api } from "./AxiosService"


class KeepsService{

    async getAllKeeps(){
        const keeps = await api.get('api/keeps')
        AppState.keeps = keeps.data
    }

}

export const keepsService = new KeepsService()