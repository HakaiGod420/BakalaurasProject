export interface Address {
  Country: string;
  City: string;
  StreetName: string;
  Province: string;
  PostalCode: string | undefined;
  HouseNumber: number | undefined;
}

export interface Invitation {
  ActiveGameId: number;
  PlayersNeeded: number;
  MinimalAge: number;
  Map_X_Cords: number;
  Map_Y_Cords: number;
  Address: Address;
  InvitationDate: string;
}

export interface UserInvitation {
  InvitationId: number;
  ActiveGameId: number;
  BoardGameTitle: string;
  BoardGameId: number;
  EventDate: string;
  EventFullLocation: string;
  MaxPlayerCount: number;
  AcceptedCount: number;
  Map_X_Cords: number;
  Map_Y_Cords: number;
}

export interface InvitationStateChange {
  InvitationId: number;
  State: string;
}

export interface SentPersonalInvitation {
  UserName: string | undefined;
  ActiveInvitationId: number;
}

export interface MeetDate {
  dateTime: string;
  timeZone: string;
}

export interface GoogleEvent {
  summary: string;
  location: string;
  start: MeetDate;
  end: MeetDate;
  reminder: Reminder;
}

export interface Reminder {
  useDefault: boolean;
  overrides: Overide[];
}

export interface Overide {
  method: string;
  minutes: number;
}

export interface InvitationItem {
  InvitationId: number;
  BoardGameId: number;
  BoardGameTitle: string;
  Date: string;
  Location: string;
  MaxPlayer: number;
  AcceptedPlayer: number;
  ImageUrl: string;
}

export interface InvitationsList {
  Invitations: InvitationItem[];
  TotalCount: number;
}

export interface Participant {
  UserId: number;
  UserName: string;
  IsBlocked: boolean;
}

export interface ParticipationState {
  UserId: number;
  ActiveGameId: number;
  IsBlocked: boolean;
}
