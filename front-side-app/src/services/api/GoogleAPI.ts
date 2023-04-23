import axios from "axios";
import { GoogleEvent } from "../types/Invitation";

const testBody = {
  start: {
    dateTime: "2023-04-10T09:00:00",
    timeZone: "America/Los_Angeles",
  },
  end: {
    dateTime: "2023-04-10T10:00:00",
    timeZone: "America/Los_Angeles",
  },
};

const GOOGLE_CALENDER_API =
  "https://www.googleapis.com/calendar/v3/calendars/primary/events";

export async function postEventInGoogleCalendar(
  accessToken: string,
  event: GoogleEvent
) {
  axios.defaults.headers.post["Authorization"] = `Bearer ${accessToken}`;
  console.log(accessToken);
  const response = await axios
    .post(GOOGLE_CALENDER_API, event)
    .catch((error) => {
      console.log(error);
    });
  return response?.data;
}
