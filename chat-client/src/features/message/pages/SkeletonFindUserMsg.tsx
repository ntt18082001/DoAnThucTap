import { useAppSelector } from 'app/hooks';
import { colorMsgDarkmode } from '../../../constants';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import React from 'react';
import { CardHeader, ListItem, Skeleton } from '@mui/material';

function SkeletonFindUserMsg() {
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const bgColorSkeleton = isDarkmode ? 'grey.700' : '';
  const bgColor = isDarkmode ? 'rgb(255,255,255,.1)' : colorMsgDarkmode;

  return (
    <ListItem alignItems="center">
      <CardHeader
        sx={{ width: '100%', backgroundColor: bgColor, mb: 0.5, padding: 0 }}
        avatar={
          <Skeleton
            animation="pulse"
            variant="circular"
            width={40}
            height={40}
            sx={{ bgcolor: bgColorSkeleton }}
          />
        }
        title={
          <Skeleton
            animation="pulse"
            height={15}
            width="80%"
            sx={{ bgcolor: bgColorSkeleton, mb: '6px' }}
          />
        }
        subheader={
          <Skeleton animation="pulse" height={12} width="50%" sx={{ bgcolor: bgColorSkeleton }} />
        }
      />
    </ListItem>
  );
}
export default React.memo(SkeletonFindUserMsg);
