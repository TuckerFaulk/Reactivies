import { Box, Button, Card, CardActions, CardContent, Chip, Typography } from "@mui/material"
import { useActivities } from "../../../lib/hooks/useActivities"
import { Link } from "react-router";

type Props = {
    activity: Activity
}

export default function ActivityCard({activity}: Props) {

    const {deleteActivity} = useActivities();

  return (
    <div>
        <Card sx={{borderRadius: 3}}>
            <CardContent>
                <Typography variant="h5">{activity.title}</Typography>
                <Typography sx={{text:'text-secondary', mb: 1}}>{activity.date}</Typography>
                <Typography variant="body2">{activity.description}</Typography>
                <Typography variant="subtitle1">{activity.city} / {activity.venue}</Typography>
            </CardContent>
            <CardActions sx={{display: 'flex', justifyContent: 'space-between', pb: 2}}>
                <Chip label={activity.category} variant="outlined" />
                <Box display='flex' gap={2}>
                    <Button 
                        component={Link}
                        to={`/activities/${activity.id}`}
                        size="medium" variant="contained">View</Button>
                    <Button 
                        onClick={() => deleteActivity.mutateAsync(activity.id)} 
                        color="error" 
                        size="medium" 
                        variant="contained"
                        disabled={deleteActivity.isPending}
                    >Delete</Button>
                </Box>
            </CardActions>
        </Card>
    </div>
  )
}