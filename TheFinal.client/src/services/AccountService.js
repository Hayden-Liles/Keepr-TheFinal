import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async getAccountVaults() {
    AppState.accountVaults = []
    const res = await api.get('/account/vaults')
    AppState.accountVaults = res.data
  }

  async getAccountKeeps() {
    AppState.keeps = []
    const res = await api.get('/account/keeps')
    AppState.keeps = res.data
  }
}

export const accountService = new AccountService()
