type Activity = {
  id: string; // Same as Id property (generated UUID)
  title: string; // Title of the activity
  date: string; // DateTime will be a string (ISO) in JSON
  description: string; // Description text
  category: string; // Category name
  isCancelled: boolean; // Cancellation flag

  // Location Props
  city: string;
  venue: string;
  latitude: number;
  longitude: number;
};
