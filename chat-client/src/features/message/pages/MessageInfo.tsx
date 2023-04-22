import { Grid } from '@mui/material';
import React from 'react';

type Props = {};

const MessageInfo = (props: Props) => {
	return (
		<Grid
			item
			xs={false}
			md={3}
			sx={{ paddingLeft: 2, paddingTop: 2 }}
		>
			Chat info
		</Grid>
	);
};

export default React.memo(MessageInfo);
