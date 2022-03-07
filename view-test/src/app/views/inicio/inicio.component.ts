import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/api/api.service';
import { character } from 'src/app/models/character';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit {

  lstPersonajesV: character[] = [];
  lstPersonajesM: character[] = [];
  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.consultarPersonajes()
  }

  consultarPersonajes() {


    this.api.get("/Personajes/consultar").subscribe(
      (res: any) => {
        console.log(res.listaVivos);
        this.lstPersonajesV = res.listaVivos;
        this.lstPersonajesM = res.listaMuertos;
        console.log(res.listaVivos[0].name);
      },
      err => {
        console.log(err);
      }
    );
  }

  generarDescripcion(id: string, nombre: string, descripcion: string, trama: any){
    var data = {
      "id": id,
      "nombre": nombre,
      "descripcion": descripcion,
      "trama": JSON.stringify(trama)
    }
    console.log(data)
    this.api.post("/Personajes/ingresar", data).subscribe(
      (res: any) => {
        console.log(res);
      },
      err => {
        console.log(err);
      }
    );
  }

}
