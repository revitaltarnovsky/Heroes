import { Hero } from './hero';
export interface TrainerMember{
  id: number;
  username: string;
  heroes: Hero[];
}