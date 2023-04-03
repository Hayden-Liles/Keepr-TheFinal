<template>
    <div class="modal" id="EditAccountModal" tabindex="-1" aria-labelledby="EditAccountModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-fullscreen-sm-down">
            <div class="modal-content">
                <div class="modal-body myFont fs-3 fw-semibold">
                    <div class="d-flex mb-2 justify-content-between">
                        <p class="">Add your Vault</p>
                        <div class="text-end">
                            <button type="button" class="mdi mdi-close mbSize" data-bs-dismiss="modal"
                                aria-label="Close"></button>
                        </div>
                    </div>
                    <form @submit.prevent="updateAccount()" class="p-2 px-3">
                        <div>
                            <label for="name">Name</label>
                            <input v-model="editable.name" type="text" required id="name" placeholder="Title...">
                            <label for="img">Profile Picture</label>
                            <input v-model="editable.picture" type="url" required id="picture" placeholder="Image url...">
                            <label for="coverImg">Cover Image</label>
                            <input v-model="editable.coverImg" type="url" required id="coverImg" placeholder="Image url...">
                        </div>
                        <div class="text-end">
                            <button class="btn btn-success text-light" data-bs-dismiss="modal" aria-label="Update">Update</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <!-- <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button> -->
</template>


<script>
import { ref, watchEffect } from 'vue'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { accountService } from '../services/AccountService'
export default {
    setup() {
        const editable = ref({})
        watchEffect(() => {
            if(AppState.account.id){
                editable.value = {...AppState.account}
            }
        })
        return {
            editable,

            async createVault() {
                try {
                    const vaultData = editable.value
                    await vaultsService.createVault(vaultData)
                    Pop.toast('Vault Created')
                }
                catch (error) {
                    logger.error(error)
                    Pop.error(error)
                }
            },

            async updateAccount(){
                const edData = editable.value
                const profileData = AppState.account
                profileData.name = edData.name
                profileData.picture = edData.picture
                profileData.coverImg = edData.coverImg
                await accountService.updateAccount(profileData)
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.myFont {
    font-family: 'Oxygen', sans-serif;
}

.mbSize {
    font-size: 1em;
    border: none;
}

input,
textarea {
    width: 100%;
    border: none !important;
    border-bottom: 1px solid black !important;
    outline: none !important;
    font-size: 1rem;
    margin-bottom: 1rem;
}

.shrink{
    width: fit-content;
    margin: 0;
}

textarea {
    margin-top: 10px;
    height: 2rem;
}

.createBtn {
    font-size: .7em;
    background-color: black;
    border: none;
    border-radius: 5px;
}
</style>