<template>
    <div class="modal" id="CreateKeepModal" tabindex="-1" aria-labelledby="CreateKeepModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-fullscreen-sm-down">
            <div class="modal-content">
                <div class="modal-body myFont fs-3 fw-semibold">
                    <div class="d-flex mb-2 justify-content-between">
                        <p class="">Add your keep</p>
                        <div class="text-end">
                            <button type="button" class="mdi mdi-close mbSize" data-bs-dismiss="modal"
                                aria-label="Close"></button>
                        </div>
                    </div>
                    <form @submit.prevent="createKeep()" class="p-2 px-3">
                        <div>
                            <input v-model="editable.name" type="text" required id="name" placeholder="Title...">
                            <input v-model="editable.img" type="url" required id="img" placeholder="Image url...">
                        </div>
                        <textarea v-model="editable.description" type="text" required id="description"
                            placeholder="Description..."></textarea>
                        <div class="text-end">
                            <button type="submit" class="createBtn text-light" data-bs-dismiss="modal" aria-label="Create">Create</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <!-- <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button> -->
</template>


<script>
import { ref } from 'vue'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
export default {
    setup() {
        const editable = ref({})
        return {
            editable,

            async createKeep() {
                try {
                    await keepsService.createKeep(editable.value)
                    editable.value = {}
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