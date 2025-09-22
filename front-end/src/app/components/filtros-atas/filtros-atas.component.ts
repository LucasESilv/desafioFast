import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-filtros-atas',
  templateUrl: './filtros-atas.component.html',
  styleUrls: ['./filtros-atas.component.scss']
})
export class FiltrosAtasComponent {
  filtroColaborador: string = '';
  filtroWorkshop: string = '';
  filtroData: string = '';

  @Output() filtrosChange = new EventEmitter<{ colaborador: string, workshop: string, data: string }>();

  emitirFiltros() {
    this.filtrosChange.emit({
      colaborador: this.filtroColaborador,
      workshop: this.filtroWorkshop,
      data: this.filtroData
    });
  }
}
