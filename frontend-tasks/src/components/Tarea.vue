<template>
    <div>
        <h1 class="display-4 text-center">
            List of tasks
        </h1>
        <hr>
        <div class="row">
            <div class="col-lg-8 offset-lg-2">
                <div class="card mt-4">
                <div class="card-body">
                    <div class="input-group">
                        <input type="text" v-model="tarea" class="form-control form-control-lg" placeholder="Add task">
                        <div class="input-group-append">
                            <button class="btn btn-success btn-lg" v-on:click="agregarTarea()">Add</button>
                        </div>
                    </div>
                    <br>
                      <div class="text-center">
                        <div v-if="loading" class="spinner-border text-success" role="status">
                            <span class="sr-only">Loading...</span>
                            </div>
                      </div>
                    <h5 v-if="listTareas.length == 0">No tasks to do</h5>
                    <ul v-if="!loading" class="list-group">
                        <li v-for="(tarea, index) of listTareas" :key="index" class="list-group-item d-flex justify-content-between">
                            <span class="cursor" v-bind:class="{'text-success': tarea.estado}" v-on:click="editarTarea(tarea, tarea.id)">
                                <i v-bind:class="[tarea.estado ? 'fa fa-check-circle' : 'far fa-circle']"></i>
                            </span>
                            {{tarea.nombre}}
                            <span class="text-danger cursor" v-on:click="eliminarTarea(tarea.id)">
                                <i class="fas fa-trash-alt" aria-hidden="true"></i>
                            </span>
                        </li>
                    </ul>
                </div>
            </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

    export default {
        name: 'Tarea',
        data(){
            return {
                tarea: '',
                listTareas: [],
                loading: false
            }
        },
        methods: {
            agregarTarea(){
                const tarea = {
                    nombre: this.tarea,
                    estado: false
                }
                this.loading = true;
                axios.post("https://localhost:7116/api/Task",tarea).then(response =>{
                    console.log(response);
                    this.obtenerTareas();
                    this.loading = false;
                }).catch(error => {
                    console.error(error);
                    this.loading = false;
                })
                this.tarea = '';
            },
            eliminarTarea(id){
                //this.listTareas.splice(index,1)
                this.loading = true;
                axios.delete("https://localhost:7116/api/Task/"+id).then(response => {
                    console.log(response);
                    this.obtenerTareas();
                    this.loading = false;
                }).catch(error => {
                    console.log(error);
                    this.loading = false;
                })
            },
            editarTarea(tarea, id){
                this.loading = true;
                tarea.estado = !tarea.estado;
                axios.put("https://localhost:7116/api/Task"+id, tarea).then(()=>{
                    this.obtenerTareas();
                    this.loading = false;
                }).catch(() => {
                    this.loading = false;
                })
            },
            obtenerTareas(){
                this.loading = true;
                axios.get("https://localhost:7116/api/Task").then(response => {
                    console.log(response);
                    this.listTareas = response.data;
                    this.loading = false;
                })
            }
        },
        created: function(){
            this.obtenerTareas();
        }
    }
</script>

<style scoped>
.cursor{
    cursor: pointer;
}
</style>