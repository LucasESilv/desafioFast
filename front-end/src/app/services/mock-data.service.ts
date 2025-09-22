import { Injectable } from '@angular/core';
import { Ata } from '../models/ata.model';
import { Colaborador } from '../models/colaborador.model';
import { Workshop } from '../models/workshop.model';

@Injectable({ providedIn: 'root' })
export class MockDataService {
  private colaboradores: Colaborador[] = [
    {id: 1, nome: 'Maria'},
    {id: 2, nome: 'José'},
    {id: 3, nome: 'Ana'}
  ];
  private workshops: Workshop[] = [
    {
      id: 1,
      nome: 'Angular Avançado',
      dataRealizacao: new Date(2025, 8, 10),
      descricao: 'Workshop técnico de Angular',
      colaboradores: [this.colaboradores[0], this.colaboradores[1]]
    },
    {
      id: 2,
      nome: 'C# Prático',
      dataRealizacao: new Date(2025, 8, 15),
      descricao: 'Workshop prático de C#',
      colaboradores: [this.colaboradores[1], this.colaboradores[2]]
    }
  ];
  private atas: Ata[] = [
    { id: 1, workshop: this.workshops[0], colaboradores: this.workshops[0].colaboradores },
    { id: 2, workshop: this.workshops[1], colaboradores: this.workshops[1].colaboradores }
  ];

  getAtas() { return this.atas; }
  getWorkshops() { return this.workshops; }
  getColaboradores() { return this.colaboradores; }
}
