import { Component, OnInit } from '@angular/core';
import { EventoService } from '../_services/Evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  
  _filtroLista: string;
  get filtroLista(){
    return this._filtroLista
  }


  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventroFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos; /*se está populada retorna uma lista, senão retorna eventos*/
  }

  eventroFiltrados: any = [];

  eventos: any = [];
  imagemLargura: number = 50;
  imagemMargem: number = 2;
  mostrarImagem: boolean = false;

  constructor(private eventoService: EventoService ) { }

  //vai ser executado antes de o html ficar pronto
  ngOnInit() {
    this.getEventos();
  }

  filtrarEventos(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(/*retorna apenas os itens do evento que estão nesse filtro*/
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos(){
    this.eventoService.getAllEvento().subscribe(response => {this.eventos = response; console.log(response);},
      error => {
        console.log(error);
      });
  }

}
