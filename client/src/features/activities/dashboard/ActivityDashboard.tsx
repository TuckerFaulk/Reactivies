import { Grid2 } from "@mui/material";
import ActivityList from "./ActivityList";
import ActivityDetail from "../details/ActivityDetail";
import ActivityForm from "../form/ActivityForm";

type Prop = {
    activities: Activity[]
    selectedActivity?: Activity
    selectActivity: (id:string) => void
    cancelActivity: () => void
    openForm: (id:string) => void
    closeForm: () => void
    editMode: boolean
    submitForm: (activity:Activity) => void
    deleteActivity: (id:string) => void
}

export default function ActivityDashboard(
  {activities, selectActivity, cancelActivity, selectedActivity,
    editMode, openForm, closeForm, submitForm, deleteActivity}: Prop) {
  return (
    <Grid2 container spacing={3}>
      <Grid2 size={7}>
        <ActivityList 
          activities={activities}
          selectActivity={selectActivity}
          deleteActivity={deleteActivity}
        />
      </Grid2>
      <Grid2 size={5}>
        {selectedActivity && !editMode &&
        <ActivityDetail 
          activity={selectedActivity} 
          cancelActivity={cancelActivity}
          openForm={openForm}
        />}
        {editMode &&
        <ActivityForm 
          closeForm={closeForm} 
          submitForm={submitForm}
          activity={selectedActivity} 
        />
        }
      </Grid2>
    </Grid2>
  );
}
