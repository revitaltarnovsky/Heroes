export interface Hero {
  id: string;
  name: string;
  ability: AbilityType;
  startTraining: Date;
  suitColors: ColorName;
  startingPower: number;
  currentPower: number;
  trainerId: number;
}

export enum AbilityType {
  Attacker = 1,
  Defender = 2,
}

export enum ColorName {
  Red = 1,
  Blue = 2,
  Yellow = 3,
  Pink = 4,
  Green = 5,
  Black = 6,
}
