import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../_services/Evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventosFiltrados: Evento[];
  eventos: Evento[];
  imagemLargura: number = 50;
  imagemMargem: number = 2;
  mostrarImagem: boolean = false;
  modalRef: BsModalRef;

  _filtroLista: string = '';

  constructor(
    private eventoService: EventoService 
    ,private modalService: BsModalService
    ) { }

  get filtroLista(): string{
    return this._filtroLista
  }


  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos; /*se está populada retorna uma lista, senão retorna eventos*/
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  ngOnInit() {
    this.getEventos();
  }

  getEventos(){
    this.eventoService.getAllEvento().subscribe((_eventos: Evento[]) => {this.eventos = _eventos; console.log(_eventos);},
      error => {
        console.log(error);
      });
  }

  //vai ser executado antes de o html ficar pronto
  

  filtrarEventos(filtrarPor: string): Evento[]{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(/*retorna apenas os itens do evento que estão nesse filtro*/
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  alternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  

}
