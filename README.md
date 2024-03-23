# Parking Barrier Manager

Parking Barrier Manager (in short PBM) is an application to remotely configure the Videx 4000 series intercoms in
use at the apartment complex 'de Grote Enk'. The intercoms control the physical barriers to grant access to the parking
facilities at the complex.

When visitors want access, they can use the intercom to contact the apartment where they need to be. The intercom
creates a GSM connection to one of several phone numbers configured in the intercom. The resident can take the call,
talk to the visitor and grant access by entering the '3' key on their phone. This is the trigger for the intercom to
open the barrier.

PBM is used to:

- Maintain a list of all apartment information for both intercoms (name, apartment number, etc).
- Maintain a list of all the phone numbers configured for each apartment
- Maintain the configuration of both intercoms (access codes and phone numbers)
- When changing the configuration for an apartment, BRS can send a SMS to the intercom to use the newly configured
  values.

When running the application for the first time, a SQLITE database will be created according to
the settings in appsettings.json.
