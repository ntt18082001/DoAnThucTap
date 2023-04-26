import SendIcon from '@mui/icons-material/Send';
import { Grid, IconButton, Paper } from '@mui/material';
import { useAppSelector } from 'app/hooks';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import React, { useState } from 'react';
import { borderColorDarkmode, borderColorDefault } from '../../../../constants';
import { CssInputBase } from '../../../../utils/CssTextField';


interface Props {
	onSubmit: (message: string) => void;
};

const ConversationFormMessage = (props: Props) => {
	const isDarkmode = useAppSelector(selectIsDarkmode);
	const [message, setMessage] = useState<string>('');

	const borderColor = isDarkmode ? borderColorDarkmode : borderColorDefault;

	return (
		<Grid
			item
			sx={{ borderTop: borderColor, maxHeight: '51px', height: '51px' }}
			display="flex"
			alignItems="flex-end"
		>
			<Paper
				component='form'
				sx={{
					display: 'flex',
					alignItems: 'center',
					bgcolor: 'transparent',
					width: '100%',
					height: '100%'
				}}
				onSubmit={(ev: React.FormEvent<HTMLFormElement>) => { 
					ev.preventDefault();
					props.onSubmit(message);
					setMessage('');
				}}
			>
				<CssInputBase
					sx={{ pl: 2, width: '100%', height: '100%', borderRight: borderColor }}
					placeholder="Aa"
					maxRows={3}
					onChange={(ev) => setMessage(ev.currentTarget.value) }
					value={message}
				/>
				<IconButton
					sx={{ p: '15px' }}
					color="secondary"
					type='submit'
				>
					<SendIcon />
				</IconButton>
			</Paper>
		</Grid>
	);
};

export default ConversationFormMessage;
