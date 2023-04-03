<template>
    <div class="modal" id="CreateVaultModal" tabindex="-1" aria-labelledby="CreateVaultModalLabel" aria-hidden="true">
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
                    <form @submit.prevent="createVault()" class="p-2 px-3">
                        <div>
                            <input v-model="editable.name" type="text" required id="name" placeholder="Title...">
                            <input v-model="editable.img" type="url" required id="img" placeholder="Image url...">
                        </div>
                        <textarea v-model="editable.description" type="text" required id="description"
                        placeholder="Description..."></textarea>
                        <div class="text-end d-flex justify-content-end align-items-center">
                            <label for="isPrivate" class="fs-5 me-2">Private</label>
                            <input v-model="editable.isPrivate" type="checkbox" class="shrink" required id="isPrivate">
                        </div>
                        <div class="text-end">
                            <button type="submit" class="createBtn text-light" data-bs-dismiss="modal" aria-label="Create">Create</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { ref } from 'vue'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
export default {
    setup() {
        const editable = ref({})
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