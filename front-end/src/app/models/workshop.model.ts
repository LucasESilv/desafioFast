import { Colaborador } from './colaborador.model';
export interface Workshop {
  id: number;
  nome: string;
  dataRealizacao: Date;
  descricao: string;
  colaboradores: Colaborador[];
}
