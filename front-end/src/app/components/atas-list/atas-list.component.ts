import { Component, OnInit } from '@angular/core';
import { Ata } from 'src/app/models/ata.model';
import { MockDataService } from 'src/app/services/mock-data.service';
import { Workshop } from 'src/app/models/workshop.model';

@Component({
  selector: 'app-atas-list',
  templateUrl: './atas-list.component.html',
  styleUrls: ['./atas-list.component.scss']
})
export class AtasListComponent implements OnInit {
  atas: Ata[] = [];
  atasFiltradas: Ata[] = [];
  workshopDetalhe: Workshop | null = null;

  constructor(private dataService: MockDataService) {}

  ngOnInit() {
    this.atas = this.dataService.getAtas();
    this.atasFiltradas = this.atas;
  }

  onFiltrosChange(filtros: { colaborador: string, workshop: string, data: string }) {
    this.atasFiltradas = this.atas.filter(ata => {
      const colaboradorMatch = filtros.colaborador
        ? ata.colaboradores.some(c =>
            c.nome.toLowerCase().includes(filtros.colaborador.toLowerCase()))
        : true;

      const workshopMatch = filtros.workshop
        ? ata.workshop.nome.toLowerCase().includes(filtros.workshop.toLowerCase())
        : true;

      const dataMatch = filtros.data
        ? this.formatDate(ata.workshop.dataRealizacao) === this.formatDate(new Date(filtros.data))
        : true;

      return colaboradorMatch && workshopMatch && dataMatch;
    });
  }

  private formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }


  abrirDetalheWorkshop(workshop: Workshop) {
    this.workshopDetalhe = workshop;
  }

  fecharDetalheWorkshop() {
    this.workshopDetalhe = null;
  }
}
